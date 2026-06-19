using Core.IGateways;
using Core.UseCases;
using Core.UseCases.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
    {
        services.AddScoped<IJoueurUseCases, JoueurUseCases>();
        services.AddScoped<IEquipeUseCases, EquipeUseCases>();
        services.AddScoped<IMatchUseCases, MatchUseCases>();
        services.AddScoped<IStaffUseCases, StaffUseCases>();
        services.AddScoped<IContratUseCases, ContratUseCases>();
        services.AddScoped<IEntrainementUseCases, EntrainementUseCases>();
        services.AddScoped<IPresenceUseCases, PresenceUseCases>();
        services.AddScoped<IStatistiqueUseCases, StatistiqueUseCases>();
        services.AddScoped<IBlessureUseCases, BlessureUseCases>();
        services.AddScoped<IAuthUseCases, AuthUseCases>();

        return services;
    }
}