using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class AuthGateway : IAuthGateway
{
    private readonly IAuthRepository _repository;

    public AuthGateway(IAuthRepository repository)
    {
        _repository = repository;
    }

    public Task<bool> ValidateCredentialsAsync(AuthRequest request) => _repository.ValidateCredentialsAsync(request);
}