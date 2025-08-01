using Domain.Entities.Wallet;
using FluentAssertions;

namespace UnitTests.Domain;

public class WalletTest
{
    [Fact]
    public void CreateWallet_ShouldInitializeWithGivenUserIdAndZeroBalance()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();

        // Act
        var wallet = new Wallet(userId);

        // Assert
        wallet.Should().NotBeNull();
        wallet.UserId.Should().Be(userId);
        wallet.Balance.Should().Be(0.0m);
    }

    [Fact]
    public void AddBalance_ShouldIncreaseBalance()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var wallet = new Wallet(userId, 100.0m);

        // Act
        wallet.AddBalance(50.0m);

        // Assert
        wallet.Balance.Should().Be(150.0m);
    }

    [Fact]
    public void DeductBalance_ShouldDecreaseBalance()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var wallet = new Wallet(userId, 100.0m);

        // Act
        wallet.DeductBalance(30.0m);

        // Assert
        wallet.Balance.Should().Be(70.0m);
    }
}