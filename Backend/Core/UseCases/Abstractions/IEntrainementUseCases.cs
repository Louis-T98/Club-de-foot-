using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IEntrainementUseCases
{
    Task<IEnumerable<Entrainement>> GetAllAsync();
    Task<Entrainement?> GetByIdAsync(int id);
    Task<int> CreateAsync(Entrainement entrainement);
    Task<bool> UpdateAsync(Entrainement entrainement);
    Task<bool> DeleteAsync(int id);
}