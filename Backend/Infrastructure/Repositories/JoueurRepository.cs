using Core.Models;
using Dapper;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Repositories;

public class JoueurRepository : IJoueurRepository
{
    private readonly DatabaseContext _context;

    public JoueurRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Joueur>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var sql = @"SELECT 
            id_joueur AS IdJoueur,
            id_equipe AS IdEquipe,
            id_poste AS IdPoste,
            nom AS Nom,
            prenom AS Prenom,
            date_naissance AS DateNaissance,
            nationalite AS Nationalite,
            numero_maillot AS NumeroMaillot
            FROM joueur";
        return await connection.QueryAsync<Joueur>(sql);
    }

    public async Task<Joueur?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = @"SELECT 
            id_joueur AS IdJoueur,
            id_equipe AS IdEquipe,
            id_poste AS IdPoste,
            nom AS Nom,
            prenom AS Prenom,
            date_naissance AS DateNaissance,
            nationalite AS Nationalite,
            numero_maillot AS NumeroMaillot
            FROM joueur WHERE id_joueur = @Id";
        return await connection.QueryFirstOrDefaultAsync<Joueur>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(Joueur joueur)
    {
        using var connection = _context.CreateConnection();
        var sql = @"INSERT INTO joueur (id_equipe, id_poste, nom, prenom, date_naissance, nationalite, numero_maillot)
                    VALUES (@IdEquipe, @IdPoste, @Nom, @Prenom, @DateNaissance, @Nationalite, @NumeroMaillot);
                    SELECT LAST_INSERT_ID();";
        return await connection.ExecuteScalarAsync<int>(sql, joueur);
    }

    public async Task<bool> UpdateAsync(Joueur joueur)
    {
        using var connection = _context.CreateConnection();
        var sql = @"UPDATE joueur SET id_equipe = @IdEquipe, id_poste = @IdPoste, nom = @Nom, 
                    prenom = @Prenom, date_naissance = @DateNaissance, nationalite = @Nationalite, 
                    numero_maillot = @NumeroMaillot WHERE id_joueur = @IdJoueur";
        var rows = await connection.ExecuteAsync(sql, joueur);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM joueur WHERE id_joueur = @Id", new { Id = id });
        return rows > 0;
    }
}