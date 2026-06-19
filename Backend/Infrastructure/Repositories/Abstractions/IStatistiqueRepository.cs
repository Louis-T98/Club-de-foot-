using Core.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IStatistiqueRepository
{
    Task<IEnumerable<Statistique>> GetByMatchAsync(int idMatch);
    Task<IEnumerable<Statistique>> GetByJoueurAsync(int idJoueur);
    Task<bool> CreateAsync(Statistique statistique);
    Task<bool> UpdateAsync(Statistique statistique);
    Task<bool> DeleteAsync(int idMatch, int idJoueur);
}