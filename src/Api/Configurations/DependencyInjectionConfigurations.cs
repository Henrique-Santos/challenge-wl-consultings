using Api.Extensions;
using Api.Services;
using Application.UseCases.Transfer;
using Application.UseCases.Wallet;
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
        services.AddScoped<IMakeTransferUseCase, MakeTransfer>();

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
}