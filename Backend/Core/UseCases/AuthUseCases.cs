using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class AuthUseCases : IAuthUseCases
{
    private readonly IAuthGateway _gateway;

    public AuthUseCases(IAuthGateway gateway)
    {
        _gateway = gateway;
    }

    public Task<bool> LoginAsync(AuthRequest request) => _gateway.ValidateCredentialsAsync(request);
}