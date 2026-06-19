using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class EntrainementGateway : IEntrainementGateway
{
    private readonly IEntrainementRepository _repository;

    public EntrainementGateway(IEntrainementRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Entrainement>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Entrainement?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task<int> CreateAsync(Entrainement entrainement) => _repository.CreateAsync(entrainement);
    public Task<bool> UpdateAsync(Entrainement entrainement) => _repository.UpdateAsync(entrainement);
    public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
}