using Microsoft.Extensions.DependencyInjection;
using InstagramAnalyzer.Application.Interfaces;
using InstagramAnalyzer.Application.Services;

namespace InstagramAnalyzer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registramos el analizador como Transient (se crea cada vez que se pide)
        services.AddTransient<IRelationshipAnalyzer, RelationshipAnalyzer>();
        return services;
    }
}