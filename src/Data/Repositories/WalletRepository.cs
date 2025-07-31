using Domain.Entities.Wallet;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly ApplicationDbContext _context;

    public WalletRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Wallet?> GetWalletByUserId(string userId)
    {
        return await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
    }

    public async Task UpdateWallet(Wallet wallet)
    {
        _context.Wallets.Update(wallet);
        await _context.SaveChangesAsync();
    }

    public async Task CreateWallet(Wallet wallet)
    {
        await _context.Wallets.AddAsync(wallet);
        await _context.SaveChangesAsync();
    }
}