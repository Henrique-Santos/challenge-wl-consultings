using Domain.Repositories;
using DomainEntity = Domain.Entities.Wallet;

namespace Application.UseCases.Wallet;

public interface ICreateWalletUseCase
{
    Task ExecuteAsync(string userId, decimal balance);
}

public class CreateWallet : ICreateWalletUseCase
{
    private readonly IWalletRepository _walletRepository;

    public CreateWallet(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task ExecuteAsync(string userId, decimal balance)
    {
        var wallet = new DomainEntity.Wallet(userId, balance);

        await _walletRepository.CreateWallet(wallet);
    }
}