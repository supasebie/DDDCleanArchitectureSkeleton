using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InPrompts.Core;

public static class DomainServiceExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services, ILogger logger)
    {
        services.AddScoped<IPromptSearchService, PromptSearchService>();
        services.AddScoped<IDeletePromptService, DeletePromptService>();

        logger.LogInformation("{Project} services registered", "Domain");

        return services;
    }
}
