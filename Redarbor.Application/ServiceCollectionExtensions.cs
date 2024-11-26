using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Redarbor.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
            });

            services.AddValidatorsFromAssemblyContaining(typeof(ServiceCollectionExtensions));

            return services;
        }
    }
}