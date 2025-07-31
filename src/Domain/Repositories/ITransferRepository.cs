using Domain.Entities.Transfer;

namespace Domain.Repositories;

public interface ITransferRepository
{
    Task<IEnumerable<Transfer>> GetTransfersByUserId(string userId, DateTime? startDate = null, DateTime? endDate = null);
    Task SaveTransfer(Transfer transfer);
}