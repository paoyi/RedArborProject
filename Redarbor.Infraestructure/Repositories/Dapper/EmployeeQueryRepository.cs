using Dapper;
using RedArbor.Domain.Employees.Entities;
using RedArbor.Domain.Employees.Repositories;

namespace Redarbor.Infraestructure.Repositories.Dapper
{
    internal class EmployeeQueryRepository : IEmployeeQueryRepository
    {
        private readonly EmployeeQueryDbContext _context;

        public EmployeeQueryRepository(EmployeeQueryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var query = @"SELECT companyId AS CompanyId,createdOn AS CreatedOn,deletedOn AS DeletedOn, email AS Email,
                        fax AS Fax,name AS name,lastLogin AS lastLogin, portalId AS portalId,roleId AS roleId,
                        statusId AS statusId, Telephone AS Telephone,updatedOn AS updatedOn,username AS username
                        FROM Employee order BY name ASC";
            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Employee>(query);
                return employees.ToList();
            }
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            var query = @"SELECT companyId AS CompanyId,createdOn AS CreatedOn,deletedOn AS DeletedOn, email AS Email,
                        fax AS Fax,name AS name,lastLogin AS lastLogin, portalId AS portalId,roleId AS roleId,
                        statusId AS statusId, Telephone AS Telephone,updatedOn AS updatedOn,username AS username
                        FROM Employee WHERE id = @Id";

            using (var connection = _context.CreateConnection())
            {
                Employee? employee = await connection.QueryFirstOrDefaultAsync<Employee>(query, new { Id = id });
                return employee;
            }
        }

        public async Task<Employee?> GetByUserAndPassword(string userName, string password)
        {
            var query = "SELECT userName FROM Employee WHERE userName = @UserName AND password = @Password";

            using (var connection = _context.CreateConnection())
            {
                Employee? employee = await connection.QueryFirstOrDefaultAsync<Employee>(query, new { UserName = userName, Password = password });
                return employee;
            }
        }
    }
}