using RedArbor.Domain.Employees.Entities;

namespace RedArbor.Domain.Employees.Repositories
{
    public interface IEmployeeQueryRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();

        Task<Employee?> GetByIdAsync(int id);

        Task<Employee?> GetByUserAndPassword(string userName, string password);
    }
}