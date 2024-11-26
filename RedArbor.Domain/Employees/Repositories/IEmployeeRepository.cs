using RedArbor.Domain.Employees.Entities;

namespace RedArbor.Domain.Employees.Repositories
{
    public interface IEmployeeRepository
    {
        Task<int> AddAsync(Employee item);

        Task DeleteAsync(int id);

        Task<Employee?> FindByIdAsync(int id);

        Task UpdateAsync(Employee item);
    }
}