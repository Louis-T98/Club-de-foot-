using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class MatchUseCases : IMatchUseCases
{
    private readonly IMatchGateway _gateway;

    public MatchUseCases(IMatchGateway gateway)
    {
        _gateway = gateway;
    }

    public Task<IEnumerable<Match>> GetAllAsync() => _gateway.GetAllAsync();
    public Task<Match?> GetByIdAsync(int id) => _gateway.GetByIdAsync(id);
    public Task<int> CreateAsync(Match match) => _gateway.CreateAsync(match);
    public Task<bool> UpdateAsync(Match match) => _gateway.UpdateAsync(match);
    public Task<bool> DeleteAsync(int id) => _gateway.DeleteAsync(id);
}