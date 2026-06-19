using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class StaffGateway : IStaffGateway
{
    private readonly IStaffRepository _repository;

    public StaffGateway(IStaffRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Staff>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Staff?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task<int> CreateAsync(Staff staff) => _repository.CreateAsync(staff);
    public Task<bool> UpdateAsync(Staff staff) => _repository.UpdateAsync(staff);
    public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
}