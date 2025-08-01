using Application.UseCases.Wallet;
using Domain.Entities.Wallet;
using Domain.Repositories;
using NSubstitute;

namespace UnitTests.Application;

public class CreateWalletTest
{
    private readonly IWalletRepository _walletRepository;

    public CreateWalletTest()
    {
        _walletRepository = Substitute.For<IWalletRepository>();
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCreateNewWalletForUser()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var usecase = new CreateWallet(_walletRepository);

        // Act
        await usecase.ExecuteAsync(userId, 100.0m);

        // Assert
        await _walletRepository.Received(1).CreateWallet(Arg.Is<Wallet>(w => w.UserId == userId && w.Balance == 100.0m));
    }
}