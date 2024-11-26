using MediatR;
using RedArbor.Domain.Employees.Entities;
using RedArbor.Domain.Employees.Repositories;

namespace Redarbor.Application.Employees.Queries
{
    public class GetAllEmployeeQuery : IRequest<List<Employee>>
    {
    }

    public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, List<Employee>>
    {
        private readonly IEmployeeQueryRepository EmployeeRepository;

        public GetAllEmployeeQueryHandler(IEmployeeQueryRepository employeeRepository)
        {
            EmployeeRepository = employeeRepository;
        }

        public async Task<List<Employee>> Handle(GetAllEmployeeQuery query, CancellationToken cancellationToken)

        {
            IEnumerable<Employee> result = await EmployeeRepository.GetAllAsync();
            return result.ToList();
        }
    }
}