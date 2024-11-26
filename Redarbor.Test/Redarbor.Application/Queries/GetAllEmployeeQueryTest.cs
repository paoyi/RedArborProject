using Moq;
using Redarbor.Application.Employees.Queries;
using RedArbor.Domain.Employees.Entities;
using RedArbor.Domain.Employees.Repositories;

namespace Redarbor.Test.Redarbor.Application.Queries
{
    [TestClass]
    public class GetAllEmployeeQueryHandlerTest
    {
        private readonly Mock<IEmployeeQueryRepository> EmployeeRepositoryMock;
        private readonly GetAllEmployeeQueryHandler Handler;

        public GetAllEmployeeQueryHandlerTest()
        {
            EmployeeRepositoryMock = new Mock<IEmployeeQueryRepository>();
            Handler = new GetAllEmployeeQueryHandler(EmployeeRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_ReturnsListOfEmployees()
        {
            List<Employee> employees =
            [
                new Employee { Name = "Employee 1" },
                new Employee { Name = "Employee 2" }
            ];

            EmployeeRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(employees);

            GetAllEmployeeQuery query = new();

            List<Employee> result = await Handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Employee 1", result[0].Name);
            Assert.AreEqual("Employee 2", result[1].Name);
        }

        [TestMethod]
        public async Task Handle_ReturnsEmptyList_WhenNoEmployeesFound()
        {
            EmployeeRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Employee>());
            GetAllEmployeeQuery query = new();

            List<Employee> result = await Handler.Handle(query, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}