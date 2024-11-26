using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using Redarbor.Application.Employees.Commands;
using RedArbor.Domain.Employees.Entities;
using RedArbor.Domain.Employees.Repositories;
using RedArbor.Domain.Exceptions;

namespace Redarbor.Test.Redarbor.Application.Commands
{
    [TestClass]
    public class AddEmployeeCommandHandlerTest
    {
        private readonly Mock<IEmployeeRepository> EmployeeRepositoryMock;
        private readonly Mock<IValidator<Employee>> ValidatorMock;
        private readonly AddEmployeeCommandHandler Handler;

        public AddEmployeeCommandHandlerTest()
        {
            EmployeeRepositoryMock = new Mock<IEmployeeRepository>();
            ValidatorMock = new Mock<IValidator<Employee>>();
            Handler = new AddEmployeeCommandHandler
            (
                EmployeeRepositoryMock.Object,
                ValidatorMock.Object
            );
        }

        [TestMethod]
        public async Task Handle_ValidCommand_ShouldAddEmployee()
        {
            AddEmployeeCommand command = new()
            {
                CompanyId = 1,
                Email = "empleado@example.com",
                Name = "Empleado 1",
                Password = "123",
                Telephone = "987654321",
                RoleId = 1,
                PortalId = 1
            };

            ValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new ValidationResult());
            EmployeeRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Employee>()))
                                   .ReturnsAsync(1);

            int result = await Handler.Handle(command, CancellationToken.None);

            Assert.AreEqual(1, result);
            EmployeeRepositoryMock.Verify(r => r.AddAsync(It.Is<Employee>(e => e.Name == command.Name)), Times.Once);
        }

        [TestMethod]
        public async Task Handle_InvalidCommand_ShouldThrowNotFoundException()
        {
            AddEmployeeCommand command = new()
            {
                CompanyId = 1,
                Email = "test",
                Name = "Empleado 1",
                Password = "123",
                Telephone = "123",
                RoleId = 1,
                PortalId = 1
            };

            ValidationResult validationResult = new([new ValidationFailure("Email", "Email is required")]);
            ValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(validationResult);

            await Assert.ThrowsExceptionAsync<NotFoundException>(() => Handler.Handle(command, CancellationToken.None));
        }
    }
}