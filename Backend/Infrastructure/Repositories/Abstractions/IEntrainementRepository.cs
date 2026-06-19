using Core.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IEntrainementRepository
{
    Task<IEnumerable<Entrainement>> GetAllAsync();
    Task<Entrainement?> GetByIdAsync(int id);
    Task<int> CreateAsync(Entrainement entrainement);
    Task<bool> UpdateAsync(Entrainement entrainement);
    Task<bool> DeleteAsync(int id);
}