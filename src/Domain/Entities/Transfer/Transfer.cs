namespace Domain.Entities.Transfer;

public class Transfer
{
    public Guid Id { get; private set; }
    public string FromUserId { get; private set; } = string.Empty;
    public string ToUserId { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public DateTime TransferDate { get; private set; }

    public Transfer(string fromUserId, string toUserId, decimal amount)
    {
        Id = Guid.NewGuid();
        FromUserId = fromUserId;
        ToUserId = toUserId;
        Amount = amount;
        TransferDate = DateTime.UtcNow;
    }
}