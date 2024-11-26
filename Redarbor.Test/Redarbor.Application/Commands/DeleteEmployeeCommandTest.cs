using Moq;
using Redarbor.Application.Employees.Commands;
using RedArbor.Domain.Employees.Entities;
using RedArbor.Domain.Employees.Repositories;
using RedArbor.Domain.Exceptions;

namespace Redarbor.Test.Redarbor.Application.Commands
{
    [TestClass]
    public class DeleteEmployeeCommandTest
    {
        private readonly Mock<IEmployeeRepository> EmployeeRepositoryMock;
        private readonly DeleteEmployeeCommandHandler Handler;

        public DeleteEmployeeCommandTest()
        {
            EmployeeRepositoryMock = new Mock<IEmployeeRepository>();
            Handler = new DeleteEmployeeCommandHandler(EmployeeRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_EmployeeExists_DeletesEmployee()
        {
            DeleteEmployeeCommand command = new() { Id = 1 };
            Employee employee = new() { };

            EmployeeRepositoryMock.Setup(r => r.FindByIdAsync(command.Id))
                .ReturnsAsync(employee);

            await Handler.Handle(command, CancellationToken.None);

            EmployeeRepositoryMock.Verify(r => r.DeleteAsync(employee.Id), Times.Once);
        }

        [TestMethod]
        public async Task Handle_EmployeeDoesNotExist_ThrowsNotFoundException()
        {
            DeleteEmployeeCommand command = new() { Id = 1 };

            EmployeeRepositoryMock.Setup(r => r.FindByIdAsync(command.Id))
                .ReturnsAsync((Employee?)null);

            await Assert.ThrowsExceptionAsync<NotFoundException>(() => Handler.Handle(command, CancellationToken.None));
        }
    }
}