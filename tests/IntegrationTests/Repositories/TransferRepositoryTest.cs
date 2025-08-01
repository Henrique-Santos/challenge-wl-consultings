using Data.Repositories;
using Domain.Entities.Transfer;
using FluentAssertions;
using IntegrationTests.Base;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Repositories;

public class TransferRepositoryTest : IClassFixture<BaseFixture>
{
    private readonly BaseFixture _fixture;

    public TransferRepositoryTest(BaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetTransfersByUserId_ShouldReturnTransfersForUser()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var transfer1 = new Transfer(userId, "recipient1", 50.0m);
        var transfer2 = new Transfer(userId, "recipient2", 75.0m);
        var repository = new TransferRepository(_fixture.Context);
        
        _fixture.Context.Transfers.AddRange(transfer1, transfer2);
        await _fixture.Context.SaveChangesAsync();

        // Act
        var transfers = await repository.GetTransfersByUserId(userId);

        // Assert
        transfers.Should().HaveCount(2);
        transfers.Should().Contain(t => t.FromUserId == userId && t.Amount == 50.0m);
        transfers.Should().Contain(t => t.FromUserId == userId && t.Amount == 75.0m);
    }

    [Fact]
    public async Task SaveTransfer_ShouldAddTransferToDatabase()
    {
        // Arrange
        var transfer = new Transfer("user1", "user2", 100.0m);
        var repository = new TransferRepository(_fixture.Context);

        // Act
        await repository.SaveTransfer(transfer);

        // Assert
        var result = await _fixture.Context.Transfers.FirstOrDefaultAsync(t => t.Id == transfer.Id);
        result.Should().NotBeNull();
        result.FromUserId.Should().Be("user1");
        result.ToUserId.Should().Be("user2");
        result.Amount.Should().Be(100.0m);
    }
}