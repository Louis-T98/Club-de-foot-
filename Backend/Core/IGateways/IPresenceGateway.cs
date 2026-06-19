using Core.Models;

namespace Core.IGateways;

public interface IPresenceGateway
{
    Task<IEnumerable<Presence>> GetByEntrainementAsync(int idEntrainement);
    Task<bool> CreateAsync(Presence presence);
    Task<bool> UpdateAsync(Presence presence);
    Task<bool> DeleteAsync(int idEntrainement, int idJoueur);
}