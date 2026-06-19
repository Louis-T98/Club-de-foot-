using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IAuthUseCases
{
    Task<bool> LoginAsync(AuthRequest request);
}