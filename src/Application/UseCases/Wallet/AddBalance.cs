using Domain.Repositories;

namespace Application.UseCases.Wallet;

public interface IAddBalanceUseCase
{
    Task ExecuteAsync(string userId, decimal amount);
}

public class AddBalance : IAddBalanceUseCase
{
    private readonly IWalletRepository _walletRepository;

    public AddBalance(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task ExecuteAsync(string userId, decimal amount)
    {
        var wallet = await _walletRepository.GetWalletByUserId(userId);

        if (wallet == null)
        {
            throw new Exception("Wallet not found for the user.");
        }

        wallet.AddBalance(amount);

        await _walletRepository.UpdateWallet(wallet);
    }
}