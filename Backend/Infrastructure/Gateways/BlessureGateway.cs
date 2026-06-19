using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class BlessureGateway : IBlessureGateway
{
    private readonly IBlessureRepository _repository;

    public BlessureGateway(IBlessureRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Blessure>> GetAllAsync() => _repository.GetAllAsync();
    public Task<IEnumerable<Blessure>> GetByJoueurAsync(int idJoueur) => _repository.GetByJoueurAsync(idJoueur);
    public Task<int> CreateAsync(Blessure blessure) => _repository.CreateAsync(blessure);
    public Task<bool> UpdateAsync(Blessure blessure) => _repository.UpdateAsync(blessure);
    public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
}