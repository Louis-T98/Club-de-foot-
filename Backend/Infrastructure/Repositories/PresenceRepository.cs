using Core.Models;
using Dapper;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Repositories;

public class PresenceRepository : IPresenceRepository
{
    private readonly DatabaseContext _context;

    public PresenceRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Presence>> GetByEntrainementAsync(int idEntrainement)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Presence>(
            "SELECT * FROM presence_entrainement WHERE id_entrainement = @Id",
            new { Id = idEntrainement });
    }

    public async Task<bool> CreateAsync(Presence presence)
    {
        using var connection = _context.CreateConnection();
        var sql = @"INSERT INTO presence_entrainement (id_entrainement, id_joueur, present)
                    VALUES (@IdEntrainement, @IdJoueur, @Present)";
        var rows = await connection.ExecuteAsync(sql, presence);
        return rows > 0;
    }

    public async Task<bool> UpdateAsync(Presence presence)
    {
        using var connection = _context.CreateConnection();
        var sql = @"UPDATE presence_entrainement SET present = @Present 
                    WHERE id_entrainement = @IdEntrainement AND id_joueur = @IdJoueur";
        var rows = await connection.ExecuteAsync(sql, presence);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int idEntrainement, int idJoueur)
    {
        using var connection = _context.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM presence_entrainement WHERE id_entrainement = @IdEntrainement AND id_joueur = @IdJoueur",
            new { IdEntrainement = idEntrainement, IdJoueur = idJoueur });
        return rows > 0;
    }
}