using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class PresenceGateway : IPresenceGateway
{
    private readonly IPresenceRepository _repository;

    public PresenceGateway(IPresenceRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Presence>> GetByEntrainementAsync(int idEntrainement) => _repository.GetByEntrainementAsync(idEntrainement);
    public Task<bool> CreateAsync(Presence presence) => _repository.CreateAsync(presence);
    public Task<bool> UpdateAsync(Presence presence) => _repository.UpdateAsync(presence);
    public Task<bool> DeleteAsync(int idEntrainement, int idJoueur) => _repository.DeleteAsync(idEntrainement, idJoueur);
}