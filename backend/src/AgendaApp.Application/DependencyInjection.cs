using Microsoft.Extensions.DependencyInjection;
using MediatR;
using AgendaApp.Application.Behaviors;
using FluentValidation;

namespace AgendaApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            // FluentValidation
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // AutoMapper
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);

            return services;
        }    
    }
}