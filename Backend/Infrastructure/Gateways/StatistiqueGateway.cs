using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class StatistiqueGateway : IStatistiqueGateway
{
    private readonly IStatistiqueRepository _repository;

    public StatistiqueGateway(IStatistiqueRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Statistique>> GetByMatchAsync(int idMatch) => _repository.GetByMatchAsync(idMatch);
    public Task<IEnumerable<Statistique>> GetByJoueurAsync(int idJoueur) => _repository.GetByJoueurAsync(idJoueur);
    public Task<bool> CreateAsync(Statistique statistique) => _repository.CreateAsync(statistique);
    public Task<bool> UpdateAsync(Statistique statistique) => _repository.UpdateAsync(statistique);
    public Task<bool> DeleteAsync(int idMatch, int idJoueur) => _repository.DeleteAsync(idMatch, idJoueur);
}