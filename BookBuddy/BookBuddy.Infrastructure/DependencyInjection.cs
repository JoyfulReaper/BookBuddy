using BookBuddy.Application.Common.Interfaces.Services;
using BookBuddy.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookBuddy.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastruction(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddPersistence();

        services.AddSingleton<IClock, Clock>();

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services;
    }
}
