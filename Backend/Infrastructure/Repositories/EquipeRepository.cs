using Core.Models;
using Dapper;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Repositories;

public class EquipeRepository : IEquipeRepository
{
    private readonly DatabaseContext _context;

    public EquipeRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Equipe>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var sql = @"SELECT 
            id_equipe AS IdEquipe,
            id_club AS IdClub,
            nom AS Nom,
            categorie AS Categorie
            FROM equipe";
        return await connection.QueryAsync<Equipe>(sql);
    }

    public async Task<Equipe?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = @"SELECT 
            id_equipe AS IdEquipe,
            id_club AS IdClub,
            nom AS Nom,
            categorie AS Categorie
            FROM equipe WHERE id_equipe = @Id";
        return await connection.QueryFirstOrDefaultAsync<Equipe>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(Equipe equipe)
    {
        using var connection = _context.CreateConnection();
        var sql = @"INSERT INTO equipe (id_club, nom, categorie)
                    VALUES (@IdClub, @Nom, @Categorie);
                    SELECT LAST_INSERT_ID();";
        return await connection.ExecuteScalarAsync<int>(sql, equipe);
    }

    public async Task<bool> UpdateAsync(Equipe equipe)
    {
        using var connection = _context.CreateConnection();
        var sql = @"UPDATE equipe SET id_club = @IdClub, nom = @Nom, 
                    categorie = @Categorie WHERE id_equipe = @IdEquipe";
        var rows = await connection.ExecuteAsync(sql, equipe);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM equipe WHERE id_equipe = @Id", new { Id = id });
        return rows > 0;
    }
}