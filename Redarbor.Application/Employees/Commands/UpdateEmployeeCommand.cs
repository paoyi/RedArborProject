using FluentValidation;
using FluentValidation.Results;
using MediatR;
using RedArbor.Domain.Employees.Entities;
using RedArbor.Domain.Employees.Repositories;
using RedArbor.Domain.Exceptions;
using System.Text.Json.Serialization;

namespace Redarbor.Application.Employees.Commands
{
    public class UpdateEmployeeCommand : IRequest
    {
        [JsonIgnore]
        public int Id { get; set; }

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

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository EmployeeRepository;
        private readonly IValidator<Employee> Validator;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IValidator<Employee> validator)
        {
            EmployeeRepository = employeeRepository;
            Validator = validator;
        }

        public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee? employeeExists = await EmployeeRepository.FindByIdAsync(request.Id) ?? throw new NotFoundException("The employee doesn't exist.");
            employeeExists.SetUpdatedEmployee(
                request.CompanyId,
                request.PortalId,
                request.RoleId,
                request.Email,
                request.Fax,
                request.Name,
                request.Password,
                request.Telephone,
                request.Username,
                request.UpdatedOn,
                request.LastLogin,
                request.StatusId
                );

            ValidationResult validationResult = await Validator.ValidateAsync(employeeExists, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new NotFoundException(string.Join('-', validationResult.Errors));
            }

            await EmployeeRepository.UpdateAsync(employeeExists);
        }
    }
}