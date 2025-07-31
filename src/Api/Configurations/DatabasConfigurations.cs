using Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Configurations;

public static class DatabaseConfigurations
{
    public static IServiceCollection AddDatabaseConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ApplicationDbContext>();

        return services;
    }
}