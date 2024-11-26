using FluentValidation;
using RedArbor.Domain.Employees.Entities;

namespace RedArbor.Domain.Employees.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.CompanyId).NotNull();
            RuleFor(e => e.Email).NotNull().EmailAddress().WithMessage("Error email address");
            RuleFor(e => e.Name).NotNull();
            RuleFor(e => e.Password).NotNull();
            RuleFor(e => e.PortalId).NotNull();
            RuleFor(e => e.RoleId).NotNull();
            RuleFor(e => e.StatusId).NotNull();
        }
    }
}