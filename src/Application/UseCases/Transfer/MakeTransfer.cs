using Domain.Repositories;
using DomainEntity = Domain.Entities.Transfer;

namespace Application.UseCases.Transfer;

public interface IMakeTransferUseCase
{
    Task ExecuteAsync(string fromUserId, string toUserId, decimal amount);
}

public class MakeTransfer : IMakeTransferUseCase
{
    private readonly IWalletRepository _walletRepository;
    private readonly ITransferRepository _transferRepository;

    public MakeTransfer(IWalletRepository walletRepository, ITransferRepository transferRepository)
    {
        _walletRepository = walletRepository;
        _transferRepository = transferRepository;
    }

    public async Task ExecuteAsync(string fromUserId, string toUserId, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than zero.");
        }

        var fromWallet = await _walletRepository.GetWalletByUserId(fromUserId);
        var toWallet = await _walletRepository.GetWalletByUserId(toUserId);

        if (fromWallet == null || toWallet == null)
        {
            throw new Exception("One or both wallets not found.");
        }

        if (fromWallet.Balance < amount)
        {
            throw new Exception("Insufficient balance in the sender's wallet.");
        }


        fromWallet.DeductBalance(amount);
        toWallet.AddBalance(amount);

        var transfer = new DomainEntity.Transfer(fromUserId, toUserId, amount);

        await _walletRepository.UpdateWallet(fromWallet);
        await _walletRepository.UpdateWallet(toWallet);
        await _transferRepository.SaveTransfer(transfer);
    }
}