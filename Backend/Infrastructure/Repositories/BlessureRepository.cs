using Core.Models;
using Dapper;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Repositories;

public class BlessureRepository : IBlessureRepository
{
    private readonly DatabaseContext _context;

    public BlessureRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Blessure>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var sql = @"SELECT 
            b.id_blessure AS IdBlessure,
            b.id_joueur AS IdJoueur,
            CONCAT(j.prenom, ' ', j.nom) AS NomJoueur,
            b.type_blessure AS TypeBlessure,
            b.date_debut AS DateDebut,
            b.date_fin AS DateFin
            FROM blessure b
            INNER JOIN joueur j ON b.id_joueur = j.id_joueur";
        return await connection.QueryAsync<Blessure>(sql);
    }

    public async Task<IEnumerable<Blessure>> GetByJoueurAsync(int idJoueur)
    {
        using var connection = _context.CreateConnection();
        var sql = @"SELECT 
            id_blessure AS IdBlessure,
            id_joueur AS IdJoueur,
            type_blessure AS TypeBlessure,
            date_debut AS DateDebut,
            date_fin AS DateFin
            FROM blessure WHERE id_joueur = @Id";
        return await connection.QueryAsync<Blessure>(sql, new { Id = idJoueur });
    }

    public async Task<int> CreateAsync(Blessure blessure)
    {
        using var connection = _context.CreateConnection();
        var sql = @"INSERT INTO blessure (id_joueur, type_blessure, date_debut, date_fin)
                    VALUES (@IdJoueur, @TypeBlessure, @DateDebut, @DateFin);
                    SELECT LAST_INSERT_ID();";
        return await connection.ExecuteScalarAsync<int>(sql, blessure);
    }

    public async Task<bool> UpdateAsync(Blessure blessure)
    {
        using var connection = _context.CreateConnection();
        var sql = @"UPDATE blessure SET 
            id_joueur = @IdJoueur, 
            type_blessure = @TypeBlessure,
            date_debut = @DateDebut, 
            date_fin = @DateFin 
            WHERE id_blessure = @IdBlessure";
        var rows = await connection.ExecuteAsync(sql, blessure);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM blessure WHERE id_blessure = @Id", new { Id = id });
        return rows > 0;
    }
}