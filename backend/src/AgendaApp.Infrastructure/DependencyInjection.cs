using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AgendaApp.Domain.Interfaces;
using AgendaApp.Infrastructure.Data;
using AgendaApp.Infrastructure.Repositories;

namespace AgendaApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            
            // DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            services.AddScoped<IContactRepository, ContactRepository>();

            return services;
        }
    }
}