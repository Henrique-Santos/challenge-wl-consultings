namespace Domain.Entities.Wallet;

public class Wallet
{
    public Guid Id { get; private set; }
    public string UserId { get; private set; } = string.Empty;
    public decimal Balance { get; private set; }

    public Wallet(string userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Balance = 0.0m;
    }

    public void AddBalance(decimal amount)
    {
        Balance += amount;
    }

    public void DeductBalance(decimal amount)
    {
        Balance -= amount;
    }
}