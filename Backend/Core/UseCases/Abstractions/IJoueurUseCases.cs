using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IJoueurUseCases
{
    Task<IEnumerable<Joueur>> GetAllAsync();
    Task<Joueur?> GetByIdAsync(int id);
    Task<int> CreateAsync(Joueur joueur);
    Task<bool> UpdateAsync(Joueur joueur);
    Task<bool> DeleteAsync(int id);
}