using Application.UseCases.Wallet;
using Domain.Entities.Wallet;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace UnitTests.Application;

public class AddBalanceTest
{
    private readonly IWalletRepository _walletRepository;

    public AddBalanceTest()
    {
        _walletRepository = Substitute.For<IWalletRepository>();
    }

    [Fact]
    public async Task ExecuteAsync_ShouldAddBalanceToExistingWallet()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var wallet = new Wallet(userId, 100.0m);
        _walletRepository.GetWalletByUserId(userId).Returns(wallet);

        var usecase = new AddBalance(_walletRepository);

        // Act
        await usecase.ExecuteAsync(userId, 50.0m);

        // Assert
        wallet.Balance.Should().Be(100.0m + 50.0m);
        await _walletRepository.Received(1).UpdateWallet(wallet);
    }
}