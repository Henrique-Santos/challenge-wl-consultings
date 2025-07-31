using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class IdentityDbHelper
{
    public static async Task EnsureDatabaseCreatedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var context = scope.ServiceProvider.GetRequiredService<IdentityContext>();

        await context.Database.MigrateAsync();

        if (context.Database.CanConnect())
        {
            await SeedIdentityTables(context, userManager);
        }
    }

    private static async Task SeedIdentityTables(IdentityContext context, UserManager<IdentityUser> userManager)
    {
        if (await context.Users.AnyAsync()) return;

        var users = Seeder.GetUsers();

        foreach (var user in users)
        {

            var userToAdd = new IdentityUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed
            };
            var result = await userManager.CreateAsync(userToAdd, user.Password);
        }
    }
}