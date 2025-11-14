using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace AgendaApp.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
        {
                // Build config
                var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "AgendaApp.API"));

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json")
                    .Build();

                var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                builder.UseNpgsql(connectionString);

                return new ApplicationDbContext(builder.Options);
            }
        }
}