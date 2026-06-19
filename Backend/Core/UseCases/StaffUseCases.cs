using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class StaffUseCases : IStaffUseCases
{
    private readonly IStaffGateway _gateway;

    public StaffUseCases(IStaffGateway gateway)
    {
        _gateway = gateway;
    }

    public Task<IEnumerable<Staff>> GetAllAsync() => _gateway.GetAllAsync();
    public Task<Staff?> GetByIdAsync(int id) => _gateway.GetByIdAsync(id);
    public Task<int> CreateAsync(Staff staff) => _gateway.CreateAsync(staff);
    public Task<bool> UpdateAsync(Staff staff) => _gateway.UpdateAsync(staff);
    public Task<bool> DeleteAsync(int id) => _gateway.DeleteAsync(id);
}