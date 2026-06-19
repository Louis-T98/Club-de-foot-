using Core.Models;
using Dapper;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Repositories;

public class StatistiqueRepository : IStatistiqueRepository
{
    private readonly DatabaseContext _context;

    public StatistiqueRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Statistique>> GetByMatchAsync(int idMatch)
    {
        using var connection = _context.CreateConnection();
        var sql = @"SELECT 
            sm.id_match AS IdMatch,
            sm.id_joueur AS IdJoueur,
            CONCAT(j.prenom, ' ', j.nom) AS NomJoueur,
            sm.buts AS Buts,
            sm.passes_decisives AS PassesDecisives,
            sm.cartons_jaunes AS CartonsJaunes,
            sm.cartons_rouges AS CartonsRouges,
            sm.minutes_jouees AS MinutesJouees
            FROM statistique_match sm
            INNER JOIN joueur j ON sm.id_joueur = j.id_joueur
            WHERE sm.id_match = @Id";
        return await connection.QueryAsync<Statistique>(sql, new { Id = idMatch });
    }

    public async Task<IEnumerable<Statistique>> GetByJoueurAsync(int idJoueur)
    {
        using var connection = _context.CreateConnection();
        var sql = @"SELECT 
            id_match AS IdMatch,
            id_joueur AS IdJoueur,
            buts AS Buts,
            passes_decisives AS PassesDecisives,
            cartons_jaunes AS CartonsJaunes,
            cartons_rouges AS CartonsRouges,
            minutes_jouees AS MinutesJouees
            FROM statistique_match WHERE id_joueur = @Id";
        return await connection.QueryAsync<Statistique>(sql, new { Id = idJoueur });
    }

    public async Task<bool> CreateAsync(Statistique statistique)
    {
        using var connection = _context.CreateConnection();
        var sql = @"INSERT INTO statistique_match (id_match, id_joueur, buts, passes_decisives, 
                    cartons_jaunes, cartons_rouges, minutes_jouees)
                    VALUES (@IdMatch, @IdJoueur, @Buts, @PassesDecisives, 
                    @CartonsJaunes, @CartonsRouges, @MinutesJouees)";
        var rows = await connection.ExecuteAsync(sql, statistique);
        return rows > 0;
    }

    public async Task<bool> UpdateAsync(Statistique statistique)
    {
        using var connection = _context.CreateConnection();
        var sql = @"UPDATE statistique_match SET 
            buts = @Buts, 
            passes_decisives = @PassesDecisives,
            cartons_jaunes = @CartonsJaunes, 
            cartons_rouges = @CartonsRouges,
            minutes_jouees = @MinutesJouees
            WHERE id_match = @IdMatch AND id_joueur = @IdJoueur";
        var rows = await connection.ExecuteAsync(sql, statistique);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int idMatch, int idJoueur)
    {
        using var connection = _context.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM statistique_match WHERE id_match = @IdMatch AND id_joueur = @IdJoueur",
            new { IdMatch = idMatch, IdJoueur = idJoueur });
        return rows > 0;
    }
}