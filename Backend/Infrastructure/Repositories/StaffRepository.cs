using Core.Models;
using Dapper;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Repositories;

public class StaffRepository : IStaffRepository
{
    private readonly DatabaseContext _context;

    public StaffRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Staff>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var sql = @"SELECT 
            id_staff AS IdStaff,
            id_equipe AS IdEquipe,
            nom AS Nom,
            prenom AS Prenom,
            role AS Role
            FROM staff";
        return await connection.QueryAsync<Staff>(sql);
    }

    public async Task<Staff?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = @"SELECT 
            id_staff AS IdStaff,
            id_equipe AS IdEquipe,
            nom AS Nom,
            prenom AS Prenom,
            role AS Role
            FROM staff WHERE id_staff = @Id";
        return await connection.QueryFirstOrDefaultAsync<Staff>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(Staff staff)
    {
        using var connection = _context.CreateConnection();
        var sql = @"INSERT INTO staff (id_equipe, nom, prenom, role)
                    VALUES (@IdEquipe, @Nom, @Prenom, @Role);
                    SELECT LAST_INSERT_ID();";
        return await connection.ExecuteScalarAsync<int>(sql, staff);
    }

    public async Task<bool> UpdateAsync(Staff staff)
    {
        using var connection = _context.CreateConnection();
        var sql = @"UPDATE staff SET 
            id_equipe = @IdEquipe,
            nom = @Nom,
            prenom = @Prenom,
            role = @Role
            WHERE id_staff = @IdStaff";
        var rows = await connection.ExecuteAsync(sql, staff);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM staff WHERE id_staff = @Id", new { Id = id });
        return rows > 0;
    }
}