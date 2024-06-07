using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using InPrompts.UseCases;
using InPrompts.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InPrompts.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(
      this IServiceCollection services,
      IConfiguration config,
      ILogger logger,
      bool isDevelopment)
    {
        if (isDevelopment)
        {
            RegisterDevelopmentOnlyDependencies(services, config, logger);

        }
        else
        {
            RegisterProductionOnlyDependencies(services);
        }

        // RegisterEF(services);

        logger.LogInformation("{Project} services registered", "Infrastructure");

        return services;
    }

    private static void RegisterDevelopmentOnlyDependencies(IServiceCollection services, IConfiguration config, ILogger logger)
    {
        string? connectionString = config.GetConnectionString("SqliteConnection");
        Guard.Against.Null(connectionString);
        services.AddDbContext<AppDbContext>(options =>
         options.UseSqlite(connectionString));

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped<IListPromptsQueryService, ListPromptsQueryService>();
        services.AddScoped<IPromptSearchService, PromptSearchService>();
        services.AddScoped<IDeletePromptService, DeletePromptService>();

        // services.Configure<MailserverConfiguration>(config.GetSection("Mailserver"));

        logger.LogInformation("Developer {Project} services registered", "Infrastructure");

    }


    private static void RegisterProductionOnlyDependencies(IServiceCollection services)
    {

        services.AddScoped<IListPromptsQueryService, ListPromptsQueryService>();

    }

    private static void RegisterEF(IServiceCollection services, ILogger logger)
    {
        logger.LogInformation("{Project} EF services registered", "Infrastructure");
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
    }
}
