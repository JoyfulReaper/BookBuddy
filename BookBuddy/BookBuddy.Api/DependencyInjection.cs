using BookBuddy.Api.Common.Mapping;

namespace BookBuddy.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddMappings();

        return services;
    }
}
