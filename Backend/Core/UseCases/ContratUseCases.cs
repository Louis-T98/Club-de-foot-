using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class ContratUseCases : IContratUseCases
{
    private readonly IContratGateway _gateway;

    public ContratUseCases(IContratGateway gateway)
    {
        _gateway = gateway;
    }

    public Task<IEnumerable<Contrat>> GetAllAsync() => _gateway.GetAllAsync();
    public Task<Contrat?> GetByIdAsync(int id) => _gateway.GetByIdAsync(id);
    public Task<int> CreateAsync(Contrat contrat) => _gateway.CreateAsync(contrat);
    public Task<bool> UpdateAsync(Contrat contrat) => _gateway.UpdateAsync(contrat);
    public Task<bool> DeleteAsync(int id) => _gateway.DeleteAsync(id);
}