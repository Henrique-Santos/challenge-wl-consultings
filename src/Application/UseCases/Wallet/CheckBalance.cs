using Domain.Repositories;

namespace Application.UseCases.Wallet;

public interface ICheckBalanceUseCase
{
    Task<decimal> ExecuteAsync(string userId);
}

public class CheckBalance : ICheckBalanceUseCase
{
    private readonly IWalletRepository _walletRepository;

    public CheckBalance(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<decimal> ExecuteAsync(string userId)
    {
        var wallet = await _walletRepository.GetWalletByUserId(userId);

        if (wallet == null)
        {
            throw new Exception("Wallet not found for the user.");
        }

        return wallet.Balance;
    }
}