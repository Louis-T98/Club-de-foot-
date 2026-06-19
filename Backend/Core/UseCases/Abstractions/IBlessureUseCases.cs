using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IBlessureUseCases
{
    Task<IEnumerable<Blessure>> GetAllAsync();
    Task<IEnumerable<Blessure>> GetByJoueurAsync(int idJoueur);
    Task<int> CreateAsync(Blessure blessure);
    Task<bool> UpdateAsync(Blessure blessure);
    Task<bool> DeleteAsync(int id);
}