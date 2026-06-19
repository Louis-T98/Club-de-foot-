using Core.Models;

namespace Core.IGateways;

public interface IContratGateway
{
    Task<IEnumerable<Contrat>> GetAllAsync();
    Task<Contrat?> GetByIdAsync(int id);
    Task<int> CreateAsync(Contrat contrat);
    Task<bool> UpdateAsync(Contrat contrat);
    Task<bool> DeleteAsync(int id);
}