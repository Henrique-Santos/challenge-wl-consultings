namespace Domain.Entities.Wallet;

public class Wallet
{
    public Guid Id { get; private set; }
    public string UserId { get; private set; } = string.Empty;
    public decimal Balance { get; private set; }

    public Wallet(string userId, decimal balance = 0.0m)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Balance = balance;
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