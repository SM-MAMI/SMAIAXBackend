using System.Diagnostics.CodeAnalysis;

using SMAIAXBackend.Application.Services.Implementations;
using SMAIAXBackend.Application.Services.Interfaces;
using SMAIAXBackend.Domain.Handlers;
using SMAIAXBackend.Infrastructure.Services;

namespace SMAIAXBackend.API.ApplicationConfigurations;

[ExcludeFromCodeCoverage]
public static class ServiceExtensions
{
    public static void AddServiceConfigurations(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IMeasurementListService, MeasurementListService>();
        services.AddScoped<IPolicyCreateService, PolicyCreateService>();
        services.AddScoped<IPolicyListService, PolicyListService>();
        services.AddScoped<ITenantContextService, TenantContextService>();
        services.AddScoped<ISmartMeterCreateService, SmartMeterCreateService>();
        services.AddScoped<ISmartMeterListService, SmartMeterListService>();
        services.AddScoped<ISmartMeterUpdateService, SmartMeterUpdateService>();
        services.AddScoped<ISmartMeterDeleteService, SmartMeterDeleteService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddTransient<IKeyGenerationService, KeyGenerationService>();
        services.AddTransient<IMeasurementHandler, MeasurementHandler>();
    }
}