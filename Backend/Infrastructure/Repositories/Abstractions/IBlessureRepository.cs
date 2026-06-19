using Core.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IBlessureRepository
{
    Task<IEnumerable<Blessure>> GetAllAsync();
    Task<IEnumerable<Blessure>> GetByJoueurAsync(int idJoueur);
    Task<int> CreateAsync(Blessure blessure);
    Task<bool> UpdateAsync(Blessure blessure);
    Task<bool> DeleteAsync(int id);
}