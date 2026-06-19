using Core.IGateways;
using Infrastructure.Gateways;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, string connectionString)
    {
        // DatabaseContext
        services.AddSingleton(new DatabaseContext(connectionString));

        // Repositories
        services.AddScoped<IJoueurRepository, JoueurRepository>();
        services.AddScoped<IEquipeRepository, EquipeRepository>();
        services.AddScoped<IMatchRepository, MatchRepository>();
        services.AddScoped<IStaffRepository, StaffRepository>();
        services.AddScoped<IContratRepository, ContratRepository>();
        services.AddScoped<IEntrainementRepository, EntrainementRepository>();
        services.AddScoped<IPresenceRepository, PresenceRepository>();
        services.AddScoped<IStatistiqueRepository, StatistiqueRepository>();
        services.AddScoped<IBlessureRepository, BlessureRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();

        // Gateways
        services.AddScoped<IJoueurGateway, JoueurGateway>();
        services.AddScoped<IEquipeGateway, EquipeGateway>();
        services.AddScoped<IMatchGateway, MatchGateway>();
        services.AddScoped<IStaffGateway, StaffGateway>();
        services.AddScoped<IContratGateway, ContratGateway>();
        services.AddScoped<IEntrainementGateway, EntrainementGateway>();
        services.AddScoped<IPresenceGateway, PresenceGateway>();
        services.AddScoped<IStatistiqueGateway, StatistiqueGateway>();
        services.AddScoped<IBlessureGateway, BlessureGateway>();
        services.AddScoped<IAuthGateway, AuthGateway>();

        return services;
    }
}