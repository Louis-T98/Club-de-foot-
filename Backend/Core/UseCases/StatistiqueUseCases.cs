using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class StatistiqueUseCases : IStatistiqueUseCases
{
    private readonly IStatistiqueGateway _gateway;

    public StatistiqueUseCases(IStatistiqueGateway gateway)
    {
        _gateway = gateway;
    }

    public Task<IEnumerable<Statistique>> GetByMatchAsync(int idMatch) => _gateway.GetByMatchAsync(idMatch);
    public Task<IEnumerable<Statistique>> GetByJoueurAsync(int idJoueur) => _gateway.GetByJoueurAsync(idJoueur);
    public Task<bool> CreateAsync(Statistique statistique) => _gateway.CreateAsync(statistique);
    public Task<bool> UpdateAsync(Statistique statistique) => _gateway.UpdateAsync(statistique);
    public Task<bool> DeleteAsync(int idMatch, int idJoueur) => _gateway.DeleteAsync(idMatch, idJoueur);
}