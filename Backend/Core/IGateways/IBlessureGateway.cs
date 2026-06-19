using Core.Models;

namespace Core.IGateways;

public interface IBlessureGateway
{
    Task<IEnumerable<Blessure>> GetAllAsync();
    Task<IEnumerable<Blessure>> GetByJoueurAsync(int idJoueur);
    Task<int> CreateAsync(Blessure blessure);
    Task<bool> UpdateAsync(Blessure blessure);
    Task<bool> DeleteAsync(int id);
}