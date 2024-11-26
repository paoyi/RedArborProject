using Azure.Core;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Redarbor.Application.Employees.Commands;
using RedArbor.Domain.Employees.Entities;
using RedArbor.Domain.Employees.Repositories;
using RedArbor.Domain.Exceptions;

namespace Redarbor.Test.Redarbor.Application.Commands
{
    [TestClass]
    public class UpdateEmployeeCommandHandlerTest
    {
        private readonly Mock<IEmployeeRepository> EmployeeRepositoryMock;
        private readonly Mock<IValidator<Employee>> ValidatorMock;
        private readonly UpdateEmployeeCommandHandler Handler;

        public UpdateEmployeeCommandHandlerTest()
        {
            EmployeeRepositoryMock = new Mock<IEmployeeRepository>();
            ValidatorMock = new Mock<IValidator<Employee>>();
            Handler = new UpdateEmployeeCommandHandler(EmployeeRepositoryMock.Object, ValidatorMock.Object);
        }

        [TestMethod]
        public async Task Handle_ValidCommand_UpdatesEmployee()
        {
            var command = new UpdateEmployeeCommand
            {
                CompanyId = 1,
                Email = "empleadoactualizado@example.com",
                Name = "Empleado 1 actualizado",
                Password = "123",
                Telephone = "987654321",
                RoleId = 1,
                PortalId = 1
            };

            Employee employee = new(
                command.CompanyId,
                command.PortalId,
                command.RoleId,
                command.Email,
                command.Fax,
                command.Name,
                command.Password,
                command.Telephone,
                command.Username,
                command.CreatedOn,
                command.LastLogin,
                command.StatusId
             );

            EmployeeRepositoryMock.Setup(r => r.FindByIdAsync(command.Id))
                .ReturnsAsync(employee);

            ValidatorMock.Setup(v => v.ValidateAsync(employee, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            await Handler.Handle(command, CancellationToken.None);

            EmployeeRepositoryMock.Verify(r => r.UpdateAsync(It.Is<Employee>(e => e.Id == command.Id)), Times.Once);
        }

        [TestMethod]
        public async Task Handle_InvalidCommand_ThrowsNotFoundException()
        {
            UpdateEmployeeCommand command = new()
            {
                Id = 1,
                CompanyId = 1,
                Email = "empleadoactualizado@example.com",
                Name = "Empleado 1 actualizado",
                Password = "123",
                Telephone = "987654321",
                RoleId = 1,
                PortalId = 1
            };

            Employee employee = new(
                 command.CompanyId,
                 command.PortalId,
                 command.RoleId,
                 command.Email,
                 command.Fax,
                 command.Name,
                 command.Password,
                 command.Telephone,
                 command.Username,
                 command.CreatedOn,
                 command.LastLogin,
                 command.StatusId
              );

            ValidationResult validationResult = new(
            [
                new ValidationFailure("Property", "Error message")
            ]);

            EmployeeRepositoryMock.Setup(r => r.FindByIdAsync(command.Id))
                .ReturnsAsync(employee);

            ValidatorMock.Setup(v => v.ValidateAsync(employee, It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            await Assert.ThrowsExceptionAsync<NotFoundException>(() => Handler.Handle(command, CancellationToken.None));
        }

        [TestMethod]
        public async Task Handle_EmployeeDoesNotExist_ThrowsNotFoundException()
        {
            UpdateEmployeeCommand command = new()
            {
                Id = 1,
                CompanyId = 1,
                Email = "empleadoactualizado@example.com",
                Fax = "123456789",
                Name = "Empleado 1 actualizado",
                Password = "123",
                PortalId = 1,
                RoleId = 1,
                Telephone = "987654321",
            };

            EmployeeRepositoryMock.Setup(r => r.FindByIdAsync(command.Id))
                .ReturnsAsync((Employee?)null);

            await Assert.ThrowsExceptionAsync<NotFoundException>(() => Handler.Handle(command, CancellationToken.None));
        }
    }
}