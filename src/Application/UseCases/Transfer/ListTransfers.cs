using Domain.Repositories;
using DomainEntity = Domain.Entities.Transfer;

namespace Application.UseCases.Transfer;

public interface IListTransfersUseCase
{
    Task<IEnumerable<DomainEntity.Transfer>> ExecuteAsync(string userId, DateTime? startDate = null, DateTime? endDate = null);
}

public class ListTransfers : IListTransfersUseCase
{
    private readonly ITransferRepository _transferRepository;

    public ListTransfers(ITransferRepository transferRepository)
    {
        _transferRepository = transferRepository;
    }

    public async Task<IEnumerable<DomainEntity.Transfer>> ExecuteAsync(string userId, DateTime? startDate = null, DateTime? endDate = null)
    {
        return await _transferRepository.GetTransfersByUserId(userId, startDate, endDate);
    }
}