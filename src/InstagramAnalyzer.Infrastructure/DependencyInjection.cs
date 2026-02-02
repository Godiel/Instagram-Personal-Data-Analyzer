using Microsoft.Extensions.DependencyInjection;
using InstagramAnalyzer.Application.Interfaces;
using InstagramAnalyzer.Infrastructure.Services;

namespace InstagramAnalyzer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IInstagramService, InstagramService>();
        return services;
    }
}