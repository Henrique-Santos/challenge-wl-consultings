using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public class ApplicationDbHelper
{
    public static async Task EnsureDatabaseCreatedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();

        if (context.Database.CanConnect())
        {
            await SeedApplicationTables(context);
        }
    }

    private static async Task SeedApplicationTables(ApplicationDbContext context)
    {
        if (!await context.Wallets.AnyAsync())
        {
            var wallets = Seeder.GetWallets();
            await context.Wallets.AddRangeAsync(wallets);
            await context.SaveChangesAsync();
        }

        if (!await context.Transfers.AnyAsync())
        {
            var transfers = Seeder.GetTransfers();
            await context.Transfers.AddRangeAsync(transfers);
            await context.SaveChangesAsync();
        }
    }
}