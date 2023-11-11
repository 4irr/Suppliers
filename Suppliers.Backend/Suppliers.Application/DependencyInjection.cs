using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Suppliers.Application.Common.Behaviors;
using Suppliers.Identity.Services;
using System.Reflection;

namespace Suppliers.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            services.AddMediatR(configuration => 
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped(typeof(EmailService));

            return services;
        }
    }
}
