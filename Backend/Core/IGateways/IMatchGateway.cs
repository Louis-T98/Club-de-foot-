using Core.Models;

namespace Core.IGateways;

public interface IMatchGateway
{
    Task<IEnumerable<Match>> GetAllAsync();
    Task<Match?> GetByIdAsync(int id);
    Task<int> CreateAsync(Match match);
    Task<bool> UpdateAsync(Match match);
    Task<bool> DeleteAsync(int id);
}