using Microsoft.EntityFrameworkCore;
using RedArbor.Domain.Employees.Entities;
using RedArbor.Domain.Employees.Repositories;

namespace Redarbor.Infraestructure.Repositories.EF
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext Context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            Context = context;
        }

        public async Task<int> AddAsync(Employee item)
        {
            EmployeeEntity entity = new();
            entity = Map(item, entity);

            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            EmployeeEntity? entity = await Context.Employees.FindAsync(id);

            Context.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<Employee?> FindByIdAsync(int id)
        {
            Employee? result = null;
            EmployeeEntity? entity = await Context.Employees.FindAsync(id);
            if (entity != null)
            {
                result = new Employee
                {
                    CreatedOn = entity.CreatedOn,
                    Id = entity.Id,
                };
            }
            return result;
        }

        public async Task UpdateAsync(Employee item)
        {
            EmployeeEntity? entity = await Context.Employees.FindAsync(item.Id);

            entity = Map(item, entity);
            Context.Update(entity);
            await Context.SaveChangesAsync();
        }

        private static EmployeeEntity Map(Employee item, EmployeeEntity entity)
        {
            entity.Id = item.Id;
            entity.CompanyId = item.CompanyId;
            entity.CreatedOn = item.CreatedOn;
            entity.DeletedOn = item.DeletedOn;
            entity.Email = item.Email;
            entity.Fax = item.Fax;
            entity.LastLogin = item.LastLogin;
            entity.Name = item.Name;
            entity.Password = item.Password;
            entity.PortalId = item.PortalId;
            entity.RoleId = item.RoleId;
            entity.StatusId = item.StatusId;
            entity.Telephone = item.Telephone;
            entity.UpdatedOn = item.UpdatedOn;
            entity.Username = item.Username;

            return entity;
        }
    }
}