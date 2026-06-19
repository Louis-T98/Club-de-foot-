using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IContratUseCases
{
    Task<IEnumerable<Contrat>> GetAllAsync();
    Task<Contrat?> GetByIdAsync(int id);
    Task<int> CreateAsync(Contrat contrat);
    Task<bool> UpdateAsync(Contrat contrat);
    Task<bool> DeleteAsync(int id);
}