using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IEquipeUseCases
{
    Task<IEnumerable<Equipe>> GetAllAsync();
    Task<Equipe?> GetByIdAsync(int id);
    Task<int> CreateAsync(Equipe equipe);
    Task<bool> UpdateAsync(Equipe equipe);
    Task<bool> DeleteAsync(int id);
}