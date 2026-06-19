using Core.Models;

namespace Core.IGateways;

public interface IEquipeGateway
{
    Task<IEnumerable<Equipe>> GetAllAsync();
    Task<Equipe?> GetByIdAsync(int id);
    Task<int> CreateAsync(Equipe equipe);
    Task<bool> UpdateAsync(Equipe equipe);
    Task<bool> DeleteAsync(int id);
}