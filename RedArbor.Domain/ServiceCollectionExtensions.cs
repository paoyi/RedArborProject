using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RedArbor.Domain.Employees.Validators;

namespace RedArbor.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(EmployeeValidator));

            return services;
        }
    }
}