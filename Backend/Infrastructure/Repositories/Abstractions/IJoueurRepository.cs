using Core.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IJoueurRepository
{
    Task<IEnumerable<Joueur>> GetAllAsync();
    Task<Joueur?> GetByIdAsync(int id);
    Task<int> CreateAsync(Joueur joueur);
    Task<bool> UpdateAsync(Joueur joueur);
    Task<bool> DeleteAsync(int id);
}