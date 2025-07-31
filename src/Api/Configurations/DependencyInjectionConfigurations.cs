using Api.Data;
using Api.Extensions;
using Api.Services;
using Application.UseCases.Transfer;
using Application.UseCases.Wallet;
using Data;
using Data.Repositories;
using Domain.Entities.User;
using Domain.Repositories;

namespace Api.Configurations;

public static class DependencyInjectionConfigurations
{
    public static IServiceCollection AddDependencyInjectionConfigurations(this IServiceCollection services)
    {
        // Use Cases
        services.AddScoped<ICheckBalanceUseCase, CheckBalance>();
        services.AddScoped<IAddBalanceUseCase, AddBalance>();
        services.AddScoped<ICreateWalletUseCase, CreateWallet>();
        services.AddScoped<IMakeTransferUseCase, MakeTransfer>();
        services.AddScoped<IListTransfersUseCase, ListTransfers>();

        // Repositories
        services.AddScoped<IWalletRepository, WalletRepository>();
        services.AddScoped<ITransferRepository, TransferRepository>();

        // Identity
        services.AddSingleton<IJwtService, JwtService>();

        // AspNet User
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AspNetUser>();

        return services;
    }
    
    public static WebApplication UseDependencyInjectionConfigurations(this WebApplication app)
    {
        IdentityDbHelper.EnsureDatabaseCreatedAsync(app.Services).Wait();

        ApplicationDbHelper.EnsureDatabaseCreatedAsync(app.Services).Wait();

        return app;
    }
}