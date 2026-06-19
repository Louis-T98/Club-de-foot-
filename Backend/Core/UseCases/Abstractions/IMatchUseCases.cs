using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IMatchUseCases
{
    Task<IEnumerable<Match>> GetAllAsync();
    Task<Match?> GetByIdAsync(int id);
    Task<int> CreateAsync(Match match);
    Task<bool> UpdateAsync(Match match);
    Task<bool> DeleteAsync(int id);
}