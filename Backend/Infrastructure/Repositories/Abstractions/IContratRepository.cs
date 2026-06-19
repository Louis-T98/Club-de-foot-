using Core.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IContratRepository
{
    Task<IEnumerable<Contrat>> GetAllAsync();
    Task<Contrat?> GetByIdAsync(int id);
    Task<int> CreateAsync(Contrat contrat);
    Task<bool> UpdateAsync(Contrat contrat);
    Task<bool> DeleteAsync(int id);
}