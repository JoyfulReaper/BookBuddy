using BookBuddy.Application.Common.Interfaces.Persistence;
using BookBuddy.Application.Common.Interfaces.Services;
using BookBuddy.Infrastructure.Persistence;
using BookBuddy.Infrastructure.Persistence.Interfaces;
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
        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
        services.AddTransient<IBookRepository, BookRepository>();
        services.AddTransient<IProgrammingLanguageRepository, ProgrammingLanguageRepository>();
        services.AddTransient<IAuthorRepository, AuthorRepository>();
        services.AddTransient<IPublisherRepository, PublisherRepository>();
        services.AddTransient<IBookFormatRepository, BookFormatRepository>();

        return services;
    }
}
