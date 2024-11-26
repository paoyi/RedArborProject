using MediatR;
using RedArbor.Domain.Employees.Entities;
using RedArbor.Domain.Employees.Repositories;

namespace Redarbor.Application.Employees.Queries
{
    public class GetByIdEmployeeQuery : IRequest<Employee>
    {
        public int Id { get; set; }
    }

    public class GetByIdEmployeeQueryHandler : IRequestHandler<GetByIdEmployeeQuery, Employee?>
    {
        private readonly IEmployeeQueryRepository EmployeeRepository;

        public GetByIdEmployeeQueryHandler(IEmployeeQueryRepository employeeRepository)
        {
            EmployeeRepository = employeeRepository;
        }

        public async Task<Employee?> Handle(GetByIdEmployeeQuery query, CancellationToken cancellationToken)
        {
            Employee result = await EmployeeRepository.GetByIdAsync(query.Id);
            if (result == null) return null;

            return result;
        }
    }
}