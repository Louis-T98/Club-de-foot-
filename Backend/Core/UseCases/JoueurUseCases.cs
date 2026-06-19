using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class JoueurUseCases : IJoueurUseCases
{
    private readonly IJoueurGateway _gateway;

    public JoueurUseCases(IJoueurGateway gateway)
    {
        _gateway = gateway;
    }

    public Task<IEnumerable<Joueur>> GetAllAsync() => _gateway.GetAllAsync();
    public Task<Joueur?> GetByIdAsync(int id) => _gateway.GetByIdAsync(id);
    public Task<int> CreateAsync(Joueur joueur) => _gateway.CreateAsync(joueur);
    public Task<bool> UpdateAsync(Joueur joueur) => _gateway.UpdateAsync(joueur);
    public Task<bool> DeleteAsync(int id) => _gateway.DeleteAsync(id);
}