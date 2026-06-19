using Core.Models;
using Dapper;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Repositories;

public class ContratRepository : IContratRepository
{
    private readonly DatabaseContext _context;

    public ContratRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contrat>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var sql = @"SELECT 
            c.id_contrat AS IdContrat,
            c.id_joueur AS IdJoueur,
            CONCAT(j.prenom, ' ', j.nom) AS NomJoueur,
            c.date_debut AS DateDebut,
            c.date_fin AS DateFin,
            c.salaire_mensuel AS SalaireMensuel
            FROM contrat c
            INNER JOIN joueur j ON c.id_joueur = j.id_joueur";
        return await connection.QueryAsync<Contrat>(sql);
    }

    public async Task<Contrat?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = @"SELECT 
            c.id_contrat AS IdContrat,
            c.id_joueur AS IdJoueur,
            CONCAT(j.prenom, ' ', j.nom) AS NomJoueur,
            c.date_debut AS DateDebut,
            c.date_fin AS DateFin,
            c.salaire_mensuel AS SalaireMensuel
            FROM contrat c
            INNER JOIN joueur j ON c.id_joueur = j.id_joueur
            WHERE c.id_contrat = @Id";
        return await connection.QueryFirstOrDefaultAsync<Contrat>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(Contrat contrat)
    {
        using var connection = _context.CreateConnection();
        var sql = @"INSERT INTO contrat (id_joueur, date_debut, date_fin, salaire_mensuel)
                    VALUES (@IdJoueur, @DateDebut, @DateFin, @SalaireMensuel);
                    SELECT LAST_INSERT_ID();";
        return await connection.ExecuteScalarAsync<int>(sql, contrat);
    }

    public async Task<bool> UpdateAsync(Contrat contrat)
    {
        using var connection = _context.CreateConnection();
        var sql = @"UPDATE contrat SET 
            id_joueur = @IdJoueur,
            date_debut = @DateDebut,
            date_fin = @DateFin,
            salaire_mensuel = @SalaireMensuel
            WHERE id_contrat = @IdContrat";
        var rows = await connection.ExecuteAsync(sql, contrat);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM contrat WHERE id_contrat = @Id", new { Id = id });
        return rows > 0;
    }
}