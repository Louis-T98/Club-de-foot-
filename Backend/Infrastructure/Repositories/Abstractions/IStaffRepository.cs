using Core.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IStaffRepository
{
    Task<IEnumerable<Staff>> GetAllAsync();
    Task<Staff?> GetByIdAsync(int id);
    Task<int> CreateAsync(Staff staff);
    Task<bool> UpdateAsync(Staff staff);
    Task<bool> DeleteAsync(int id);
}