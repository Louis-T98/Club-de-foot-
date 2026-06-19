using Core.Models;
using Core.UseCases.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.EndPoints;

public static class AuthEndPoints
{
    public static void MapAuthEndPoints(this WebApplication app)
    {
        app.MapPost("/api/auth/login", async (AuthRequest request, IAuthUseCases useCases, IConfiguration config) =>
        {
            var isValid = await useCases.LoginAsync(request);
            if (!isValid) return Results.Unauthorized();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: new[] { new Claim(ClaimTypes.Name, request.Username!) },
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds
            );

            return Results.Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        });
    }
}