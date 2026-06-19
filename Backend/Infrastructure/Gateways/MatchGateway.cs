using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class MatchGateway : IMatchGateway
{
    private readonly IMatchRepository _repository;

    public MatchGateway(IMatchRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Match>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Match?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task<int> CreateAsync(Match match) => _repository.CreateAsync(match);
    public Task<bool> UpdateAsync(Match match) => _repository.UpdateAsync(match);
    public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
}