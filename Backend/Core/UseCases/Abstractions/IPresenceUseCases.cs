using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IPresenceUseCases
{
    Task<IEnumerable<Presence>> GetByEntrainementAsync(int idEntrainement);
    Task<bool> CreateAsync(Presence presence);
    Task<bool> UpdateAsync(Presence presence);
    Task<bool> DeleteAsync(int idEntrainement, int idJoueur);
}