using Domain.Entities.Transfer;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class TransferRepository : ITransferRepository
{
    private readonly ApplicationDbContext _context;

    public TransferRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Transfer>> GetTransfersByUserId(string userId, DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.Transfers.AsQueryable();

        query = query.Where(t => t.FromUserId == userId);

        if (startDate.HasValue)
        {
            query = query.Where(t => t.TransferDate >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(t => t.TransferDate <= endDate.Value);
        }

        return await query.ToListAsync();
    }

    public async Task SaveTransfer(Transfer transfer)
    {
        _context.Transfers.Add(transfer);
        await _context.SaveChangesAsync();
    }
}