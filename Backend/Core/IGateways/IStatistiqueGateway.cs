using Core.Models;

namespace Core.IGateways;

public interface IStatistiqueGateway
{
    Task<IEnumerable<Statistique>> GetByMatchAsync(int idMatch);
    Task<IEnumerable<Statistique>> GetByJoueurAsync(int idJoueur);
    Task<bool> CreateAsync(Statistique statistique);
    Task<bool> UpdateAsync(Statistique statistique);
    Task<bool> DeleteAsync(int idMatch, int idJoueur);
}