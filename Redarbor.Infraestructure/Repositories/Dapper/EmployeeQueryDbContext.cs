using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Redarbor.Infraestructure.Repositories.Dapper
{
    internal class EmployeeQueryDbContext
    {
        private readonly IConfiguration Configuration;
        private readonly string? ConnectionString;

        public EmployeeQueryDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(ConnectionString);
    }
}