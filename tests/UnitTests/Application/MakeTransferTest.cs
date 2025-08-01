using Application.UseCases.Transfer;
using Domain.Entities.Wallet;
using Domain.Repositories;
using NSubstitute;

namespace UnitTests.Application;

public class MakeTransferTest
{
    private readonly IWalletRepository _walletRepository;
    private readonly ITransferRepository _transferRepository;

    public MakeTransferTest()
    {
        _walletRepository = Substitute.For<IWalletRepository>();
        _transferRepository = Substitute.For<ITransferRepository>();
    }

    [Fact]
    public async Task ExecuteAsync_ShouldMakeTransferBetweenWallets()
    {
        // Arrange
        var fromUserId = Guid.NewGuid().ToString();
        var toUserId = Guid.NewGuid().ToString();
        var amount = 50.0m;
        var fromWallet = new Wallet(fromUserId, 100.0m);
        var toWallet = new Wallet(toUserId, 50.0m);
        var usecase = new MakeTransfer(_walletRepository, _transferRepository);
        _walletRepository.GetWalletByUserId(fromUserId).Returns(fromWallet);
        _walletRepository.GetWalletByUserId(toUserId).Returns(toWallet);

        // Act
        await usecase.ExecuteAsync(fromUserId, toUserId, amount);

        // Assert
        await _walletRepository.Received(1).UpdateWallet(Arg.Is<Wallet>(w => w.UserId == fromUserId && w.Balance == 50.0m));
        await _walletRepository.Received(1).UpdateWallet(Arg.Is<Wallet>(w => w.UserId == toUserId && w.Balance == 100.0m));
    }
}