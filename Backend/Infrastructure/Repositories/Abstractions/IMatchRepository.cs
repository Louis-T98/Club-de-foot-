using Core.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IMatchRepository
{
    Task<IEnumerable<Match>> GetAllAsync();
    Task<Match?> GetByIdAsync(int id);
    Task<int> CreateAsync(Match match);
    Task<bool> UpdateAsync(Match match);
    Task<bool> DeleteAsync(int id);
}