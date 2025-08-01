using Application.UseCases.Wallet;
using Domain.Entities.Wallet;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace UnitTests.Application;

public class CheckBalanceTest
{
    private readonly IWalletRepository _walletRepository;

    public CheckBalanceTest()
    {
        _walletRepository = Substitute.For<IWalletRepository>();
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnBalanceOfExistingWallet()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var wallet = new Wallet(userId, 100.0m);
        _walletRepository.GetWalletByUserId(userId).Returns(wallet);

        var usecase = new CheckBalance(_walletRepository);

        // Act
        var balance = await usecase.ExecuteAsync(userId);

        // Assert
        balance.Should().Be(100.0m);
        await _walletRepository.Received(1).GetWalletByUserId(userId);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowExceptionIfWalletNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        _walletRepository.GetWalletByUserId(userId).Returns((Wallet)null!);

        var usecase = new CheckBalance(_walletRepository);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => usecase.ExecuteAsync(userId));
        await _walletRepository.Received(1).GetWalletByUserId(userId);
    }
}