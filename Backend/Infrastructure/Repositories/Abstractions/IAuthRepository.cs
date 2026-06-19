using Core.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IAuthRepository
{
    Task<bool> ValidateCredentialsAsync(AuthRequest request);
}