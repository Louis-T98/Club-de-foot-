using Core.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IPresenceRepository
{
    Task<IEnumerable<Presence>> GetByEntrainementAsync(int idEntrainement);
    Task<bool> CreateAsync(Presence presence);
    Task<bool> UpdateAsync(Presence presence);
    Task<bool> DeleteAsync(int idEntrainement, int idJoueur);
}