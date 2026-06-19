using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class JoueurGateway : IJoueurGateway
{
    private readonly IJoueurRepository _repository;

    public JoueurGateway(IJoueurRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Joueur>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Joueur?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task<int> CreateAsync(Joueur joueur) => _repository.CreateAsync(joueur);
    public Task<bool> UpdateAsync(Joueur joueur) => _repository.UpdateAsync(joueur);
    public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
}