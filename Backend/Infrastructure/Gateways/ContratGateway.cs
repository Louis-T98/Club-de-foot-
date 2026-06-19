using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class ContratGateway : IContratGateway
{
    private readonly IContratRepository _repository;

    public ContratGateway(IContratRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Contrat>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Contrat?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task<int> CreateAsync(Contrat contrat) => _repository.CreateAsync(contrat);
    public Task<bool> UpdateAsync(Contrat contrat) => _repository.UpdateAsync(contrat);
    public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
}