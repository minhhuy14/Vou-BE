using AuthServer.Repositories;
using AuthServer.Repositories.Implements;
using AuthServer.Repositories.Interfaces;
using AuthServer.Services.EmailService;
using AuthServer.Services.PubSubService;

namespace AuthServer.StartupRegistrations;

public static class CustomDIRegistrations
{
    public static IServiceCollection ConfigureDIServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<ICounterPartRepository, CounterPartRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddScoped<IEventPublishService, EventPublishService>();
        return services;
    }
}