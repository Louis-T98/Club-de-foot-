using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class BlessureUseCases : IBlessureUseCases
{
    private readonly IBlessureGateway _gateway;

    public BlessureUseCases(IBlessureGateway gateway)
    {
        _gateway = gateway;
    }

    public Task<IEnumerable<Blessure>> GetAllAsync() => _gateway.GetAllAsync();
    public Task<IEnumerable<Blessure>> GetByJoueurAsync(int idJoueur) => _gateway.GetByJoueurAsync(idJoueur);
    public Task<int> CreateAsync(Blessure blessure) => _gateway.CreateAsync(blessure);
    public Task<bool> UpdateAsync(Blessure blessure) => _gateway.UpdateAsync(blessure);
    public Task<bool> DeleteAsync(int id) => _gateway.DeleteAsync(id);
}