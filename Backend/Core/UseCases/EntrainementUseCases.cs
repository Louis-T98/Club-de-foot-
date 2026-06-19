using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class EntrainementUseCases : IEntrainementUseCases
{
    private readonly IEntrainementGateway _gateway;

    public EntrainementUseCases(IEntrainementGateway gateway)
    {
        _gateway = gateway;
    }

    public Task<IEnumerable<Entrainement>> GetAllAsync() => _gateway.GetAllAsync();
    public Task<Entrainement?> GetByIdAsync(int id) => _gateway.GetByIdAsync(id);
    public Task<int> CreateAsync(Entrainement entrainement) => _gateway.CreateAsync(entrainement);
    public Task<bool> UpdateAsync(Entrainement entrainement) => _gateway.UpdateAsync(entrainement);
    public Task<bool> DeleteAsync(int id) => _gateway.DeleteAsync(id);
}