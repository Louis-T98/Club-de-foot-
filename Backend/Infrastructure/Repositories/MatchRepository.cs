using Core.Models;
using Dapper;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Repositories;

public class MatchRepository : IMatchRepository
{
    private readonly DatabaseContext _context;

    public MatchRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Match>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var sql = @"SELECT 
            id_match AS IdMatch,
            id_equipe AS IdEquipe,
            adversaire AS Adversaire,
            date_match AS DateMatch,
            score_pour AS ScorePour,
            score_contre AS ScoreContre
            FROM match_foot";
        return await connection.QueryAsync<Match>(sql);
    }

    public async Task<Match?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = @"SELECT 
            id_match AS IdMatch,
            id_equipe AS IdEquipe,
            adversaire AS Adversaire,
            date_match AS DateMatch,
            score_pour AS ScorePour,
            score_contre AS ScoreContre
            FROM match_foot WHERE id_match = @Id";
        return await connection.QueryFirstOrDefaultAsync<Match>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(Match match)
    {
        using var connection = _context.CreateConnection();
        var sql = @"INSERT INTO match_foot (id_equipe, adversaire, date_match, score_pour, score_contre)
                    VALUES (@IdEquipe, @Adversaire, @DateMatch, @ScorePour, @ScoreContre);
                    SELECT LAST_INSERT_ID();";
        return await connection.ExecuteScalarAsync<int>(sql, match);
    }

    public async Task<bool> UpdateAsync(Match match)
    {
        using var connection = _context.CreateConnection();
        var sql = @"UPDATE match_foot SET id_equipe = @IdEquipe, adversaire = @Adversaire, 
                    date_match = @DateMatch, score_pour = @ScorePour, 
                    score_contre = @ScoreContre WHERE id_match = @IdMatch";
        var rows = await connection.ExecuteAsync(sql, match);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM match_foot WHERE id_match = @Id", new { Id = id });
        return rows > 0;
    }
}