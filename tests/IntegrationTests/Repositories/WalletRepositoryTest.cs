using Data.Repositories;
using Domain.Entities.Wallet;
using FluentAssertions;
using IntegrationTests.Base;

namespace IntegrationTests.Repositories;

public class WalletRepositoryTest : IClassFixture<BaseFixture>
{
    private readonly BaseFixture _fixture;

    public WalletRepositoryTest(BaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task CreateWallet_ShouldAddNewWalletToDatabase()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var wallet = new Wallet(userId, 100.0m);
        var repository = new WalletRepository(_fixture.Context);

        // Act
        await repository.CreateWallet(wallet);

        // Assert
        var result = await repository.GetWalletByUserId(userId);

        result.Should().NotBeNull();
        result.UserId.Should().Be(userId);
        result.Balance.Should().Be(100.0m);
    }

    [Fact]
    public async Task UpdateWallet_ShouldUpdateWalletToDatabase()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var wallet = new Wallet(userId, 100.0m);
        var repository = new WalletRepository(_fixture.Context);
        _fixture.Context.Wallets.Add(wallet);
        _fixture.Context.SaveChanges();

        // Act
        wallet.AddBalance(100.0m);
        await repository.UpdateWallet(wallet);

        // Assert
        var result = await repository.GetWalletByUserId(userId);

        result.Should().NotBeNull();
        result.Balance.Should().Be(200.0m);
    }

    [Fact]
    public async Task GetWalletByUserId_ShouldReturnWalletIfExists()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var wallet = new Wallet(userId, 100.0m);
        var repository = new WalletRepository(_fixture.Context);
        _fixture.Context.Wallets.Add(wallet);
        _fixture.Context.SaveChanges();

        // Act
        var result = await repository.GetWalletByUserId(userId);

        // Assert
        result.Should().NotBeNull();
        result.UserId.Should().Be(userId);
        result.Balance.Should().Be(100.0m);
    }
}