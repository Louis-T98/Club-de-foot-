using Core.Models;
using Dapper;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Repositories;

public class EntrainementRepository : IEntrainementRepository
{
    private readonly DatabaseContext _context;

    public EntrainementRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Entrainement>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var sql = @"SELECT 
            e.id_entrainement AS IdEntrainement,
            e.id_equipe AS IdEquipe,
            eq.nom AS NomEquipe,
            e.date_entrainement AS DateEntrainement,
            e.duree_minutes AS DureeMinutes,
            e.theme AS Theme
            FROM entrainement e
            INNER JOIN equipe eq ON e.id_equipe = eq.id_equipe";
        return await connection.QueryAsync<Entrainement>(sql);
    }

    public async Task<Entrainement?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = @"SELECT 
            id_entrainement AS IdEntrainement,
            id_equipe AS IdEquipe,
            date_entrainement AS DateEntrainement,
            duree_minutes AS DureeMinutes,
            theme AS Theme
            FROM entrainement WHERE id_entrainement = @Id";
        return await connection.QueryFirstOrDefaultAsync<Entrainement>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(Entrainement entrainement)
    {
        using var connection = _context.CreateConnection();
        var sql = @"INSERT INTO entrainement (id_equipe, date_entrainement, duree_minutes, theme)
                    VALUES (@IdEquipe, @DateEntrainement, @DureeMinutes, @Theme);
                    SELECT LAST_INSERT_ID();";
        return await connection.ExecuteScalarAsync<int>(sql, entrainement);
    }

    public async Task<bool> UpdateAsync(Entrainement entrainement)
    {
        using var connection = _context.CreateConnection();
        var sql = @"UPDATE entrainement SET 
            id_equipe = @IdEquipe, 
            date_entrainement = @DateEntrainement, 
            duree_minutes = @DureeMinutes, 
            theme = @Theme 
            WHERE id_entrainement = @IdEntrainement";
        var rows = await connection.ExecuteAsync(sql, entrainement);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM entrainement WHERE id_entrainement = @Id", new { Id = id });
        return rows > 0;
    }
}