using Moq;
using Redarbor.Application.Authentication.Queries;
using RedArbor.Domain.Employees.Entities;
using RedArbor.Domain.Employees.Repositories;
using RedArbor.Domain.Exceptions;
using Redarbor.Infraestructure.Security.TokenGenerator;

namespace Redarbor.Test.Redarbor.Application.Queries
{
    [TestClass]
    public class GenerateTokenQueryHandlerTest
    {
        private readonly Mock<IJwtTokenGenerator> JwtTokenGeneratorMock;
        private readonly Mock<IEmployeeQueryRepository> EmployeeQueryRepositoryMock;
        private readonly GenerateTokenQueryHandler Handler;

        public GenerateTokenQueryHandlerTest()
        {
            JwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
            EmployeeQueryRepositoryMock = new Mock<IEmployeeQueryRepository>();
            Handler = new GenerateTokenQueryHandler(JwtTokenGeneratorMock.Object, EmployeeQueryRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_ValidCredentials_ReturnsToken()
        {
            GenerateTokenQuery query = new() { Username = "testuser", Password = "password" };
            Employee employee = new() { Id = 1, Name = "Test User" };
            string token = "generated_token";

            EmployeeQueryRepositoryMock.Setup(r => r.GetByUserAndPassword(query.Username, query.Password))
                .ReturnsAsync(employee);

            JwtTokenGeneratorMock.Setup(g => g.GenerateToken(It.IsAny<Guid>(), query.Username))
                .Returns(token);

            var result = await Handler.Handle(query, CancellationToken.None);

            Assert.AreEqual(token, result);
        }

        [TestMethod]
        public async Task Handle_InvalidCredentials_ThrowsNotFoundException()
        {
            var query = new GenerateTokenQuery { Username = "testuser", Password = "wrongpassword" };

            EmployeeQueryRepositoryMock.Setup(r => r.GetByUserAndPassword(query.Username, query.Password))
                .ReturnsAsync((Employee?)null);

            await Assert.ThrowsExceptionAsync<NotFoundException>(() => Handler.Handle(query, CancellationToken.None));
        }
    }
}