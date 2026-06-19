using Core.Models;
using Dapper;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly DatabaseContext _context;

    public AuthRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<bool> ValidateCredentialsAsync(AuthRequest request)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT COUNT(*) FROM admin WHERE username = @Username AND password = @Password";
        var count = await connection.ExecuteScalarAsync<int>(sql, new
        {
            Username = request.Username,
            Password = request.Password
        });
        return count > 0;
    }
}