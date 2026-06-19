using Core.Models;

namespace Core.IGateways;

public interface IAuthGateway
{
    Task<bool> ValidateCredentialsAsync(AuthRequest request);
}