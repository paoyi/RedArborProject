using Moq;
using Redarbor.Application.Employees.Queries;
using RedArbor.Domain.Employees.Entities;
using RedArbor.Domain.Employees.Repositories;

namespace Redarbor.Test.Redarbor.Application.Queries
{
    public class GetByIdEmployeeQueryHandlerTest
    {
        private readonly Mock<IEmployeeQueryRepository> EmployeeRepositoryMock;
        private readonly GetByIdEmployeeQueryHandler Handler;

        public GetByIdEmployeeQueryHandlerTest()
        {
            EmployeeRepositoryMock = new Mock<IEmployeeQueryRepository>();
            Handler = new GetByIdEmployeeQueryHandler(EmployeeRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_EmployeeExists_ReturnsEmployee()
        {
            GetByIdEmployeeQuery query = new() { Id = 1 };
            Employee employee = new() { Name = "Test Employee" };

            EmployeeRepositoryMock.Setup(r => r.GetByIdAsync(query.Id))
                .ReturnsAsync(employee);

            Employee? result = await Handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(employee.Id, result.Id);
            Assert.AreEqual(employee.Name, result.Name);
        }

        [TestMethod]
        public async Task Handle_EmployeeDoesNotExist_ReturnsNull()
        {
            GetByIdEmployeeQuery query = new() { Id = 1 };

            Employee? employee = null;
            EmployeeRepositoryMock.Setup(r => r.GetByIdAsync(query.Id))
                 .ReturnsAsync(employee);

            Employee? result = await Handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
        }
    }
}