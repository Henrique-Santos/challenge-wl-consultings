using Application.UseCases.Transfer;
using Domain.Entities.Transfer;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace UnitTests.Application;

public class ListTransfersTest
{
    private readonly ITransferRepository _transferRepository;

    public ListTransfersTest()
    {
        _transferRepository = Substitute.For<ITransferRepository>();
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnTransfersForUser()
    {
        // Arrange
        var fromUserId = Guid.NewGuid().ToString();
        var toUserId = Guid.NewGuid().ToString();
        var transfers = new List<Transfer>
        {
            new(fromUserId, toUserId, 100.0m),
        };
        _transferRepository.GetTransfersByUserId(fromUserId, null, null).Returns(transfers);

        var usecase = new ListTransfers(_transferRepository);

        // Act
        var result = await usecase.ExecuteAsync(fromUserId);

        // Assert
        result.Should().BeEquivalentTo(transfers);
        await _transferRepository.Received(1).GetTransfersByUserId(fromUserId, null, null);
    }
}