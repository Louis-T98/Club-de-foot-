using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class EquipeUseCases : IEquipeUseCases
{
    private readonly IEquipeGateway _gateway;

    public EquipeUseCases(IEquipeGateway gateway)
    {
        _gateway = gateway;
    }

    public Task<IEnumerable<Equipe>> GetAllAsync() => _gateway.GetAllAsync();
    public Task<Equipe?> GetByIdAsync(int id) => _gateway.GetByIdAsync(id);
    public Task<int> CreateAsync(Equipe equipe) => _gateway.CreateAsync(equipe);
    public Task<bool> UpdateAsync(Equipe equipe) => _gateway.UpdateAsync(equipe);
    public Task<bool> DeleteAsync(int id) => _gateway.DeleteAsync(id);
}