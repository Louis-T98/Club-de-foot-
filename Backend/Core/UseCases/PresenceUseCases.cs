using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class PresenceUseCases : IPresenceUseCases
{
    private readonly IPresenceGateway _gateway;

    public PresenceUseCases(IPresenceGateway gateway)
    {
        _gateway = gateway;
    }

    public Task<IEnumerable<Presence>> GetByEntrainementAsync(int idEntrainement) => _gateway.GetByEntrainementAsync(idEntrainement);
    public Task<bool> CreateAsync(Presence presence) => _gateway.CreateAsync(presence);
    public Task<bool> UpdateAsync(Presence presence) => _gateway.UpdateAsync(presence);
    public Task<bool> DeleteAsync(int idEntrainement, int idJoueur) => _gateway.DeleteAsync(idEntrainement, idJoueur);
}