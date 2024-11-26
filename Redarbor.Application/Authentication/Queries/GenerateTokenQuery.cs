using MediatR;
using Redarbor.Infraestructure.Security.TokenGenerator;
using RedArbor.Domain.Employees.Entities;
using RedArbor.Domain.Employees.Repositories;
using RedArbor.Domain.Exceptions;

namespace Redarbor.Application.Authentication.Queries
{
    public class GenerateTokenQuery : IRequest<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class GenerateTokenQueryHandler : IRequestHandler<GenerateTokenQuery, string>
    {
        private readonly IJwtTokenGenerator JwtTokenGenerator;
        private readonly IEmployeeQueryRepository EmployeeQueryRepository;

        public GenerateTokenQueryHandler(IJwtTokenGenerator jwtTokenGenerator,
            IEmployeeQueryRepository employeeQueryRepository)
        {
            JwtTokenGenerator = jwtTokenGenerator;
            EmployeeQueryRepository = employeeQueryRepository;
        }

        public Task<string> Handle(GenerateTokenQuery query, CancellationToken cancellationToken)
        {
            Employee? employee = EmployeeQueryRepository.GetByUserAndPassword(query.Username, query.Password).Result;
            if (employee != null)
            {
                Guid id = Guid.NewGuid();

                string token = JwtTokenGenerator.GenerateToken(id, query.Username);
                return Task.FromResult(token);
            }
            else
            {
                throw new NotFoundException("Error generate token.");
            }
        }
    }
}