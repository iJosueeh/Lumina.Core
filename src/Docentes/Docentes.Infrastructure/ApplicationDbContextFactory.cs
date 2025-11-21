using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MediatR;

namespace Docentes.Infrastructure
{
    // Simple NullPublisher for design-time operations
    public class NullPublisher : IPublisher
    {
        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            return Task.CompletedTask;
        }
    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Build configuration
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("Database");

            if (string.IsNullOrEmpty(connectionString))
            {
                // Fallback for design-time
                connectionString = "Host=localhost;Port=5432;Database=design_time_db;Username=postgres;Password=password";
            }

            builder.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();

            // Provide a NullPublisher for design-time
            var publisher = new NullPublisher();

            return new ApplicationDbContext(builder.Options, publisher);
        }
    }
}