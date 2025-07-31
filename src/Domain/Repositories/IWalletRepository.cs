using Domain.Entities.Wallet;

namespace Domain.Repositories;

public interface IWalletRepository
{
    Task<Wallet?> GetWalletByUserId(string userId);

    Task UpdateWallet(Wallet wallet);

    Task CreateWallet(Wallet wallet);
}