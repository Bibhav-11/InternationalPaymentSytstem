using Application.Interfaces;
using Application.Services;
using Infrastructure;
using InternationalPaymentTransfer.ClaimsTransformation;
using InternationalPaymentTransfer.Services;
using Microsoft.AspNetCore.Authentication;

namespace InternationalPaymentTransfer.Startup;

public static class DependenciesRegistration
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IClaimsTransformation, CustomClaimsTransformation>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IExchangeRateService, ExchangeRateService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<ITransactionService, TransactionService>();
    }
}