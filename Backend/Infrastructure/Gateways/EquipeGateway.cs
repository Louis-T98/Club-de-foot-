using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class EquipeGateway : IEquipeGateway
{
    private readonly IEquipeRepository _repository;

    public EquipeGateway(IEquipeRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Equipe>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Equipe?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task<int> CreateAsync(Equipe equipe) => _repository.CreateAsync(equipe);
    public Task<bool> UpdateAsync(Equipe equipe) => _repository.UpdateAsync(equipe);
    public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
}