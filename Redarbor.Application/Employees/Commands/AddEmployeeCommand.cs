using FluentValidation;
using FluentValidation.Results;
using MediatR;
using RedArbor.Domain.Employees.Entities;
using RedArbor.Domain.Employees.Repositories;
using RedArbor.Domain.Exceptions;

namespace Redarbor.Application.Employees.Commands
{
    public class AddEmployeeCommand : IRequest<int>
    {
        public int CompanyId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string Email { get; set; }

        public string? Fax { get; set; }

        public string Name { get; set; }

        public DateTime? LastLogin { get; set; }

        public string Password { get; set; }

        public int PortalId { get; set; }

        public int RoleId { get; set; }

        public EmployeeStatus StatusId { get; set; }

        public string? Telephone { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string Username { get; set; }
    }

    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, int>
    {
        private readonly IEmployeeRepository EmployeeRepository;
        private readonly IValidator<Employee> Validator;

        public AddEmployeeCommandHandler(IEmployeeRepository employeeRepository, IValidator<Employee> validator)
        {
            EmployeeRepository = employeeRepository;
            Validator = validator;
        }

        public async Task<int> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var newEmployee = new Employee(
                request.CompanyId,
                request.PortalId,
                request.RoleId,
                request.Email,
                request.Fax,
                request.Name,
                request.Password,
                request.Telephone,
                request.Username,
                request.CreatedOn,
                request.LastLogin,
                request.StatusId
               );

            ValidationResult validationResult = await Validator.ValidateAsync(newEmployee, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new NotFoundException(string.Join('-', validationResult.Errors));
            }

            int result = await EmployeeRepository.AddAsync(newEmployee);
            return result;
        }
    }
}