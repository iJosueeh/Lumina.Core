using Carreras.Domain.Carreras;
using Carreras.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Serilog;

namespace Carreras.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration["MongoDbSettings:ConnectionString"] ??
            throw new ArgumentNullException("MongoDbSettings:ConnectionString not found in configuration.");
        var databaseName = configuration["CarrerasDatabase"] ??
            throw new ArgumentNullException("CarrerasDatabase not found in configuration.");

        services.AddSingleton<IMongoClient>(sp =>
        {
            return new MongoClient(connectionString);
        });

        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(databaseName);
        });

        services.AddScoped(sp =>
        {
            var database = sp.GetRequiredService<IMongoDatabase>();
            return database.GetCollection<Carrera>("carreras");
        });

        services.AddScoped<ICarreraRepository, CarreraRepository>();

        return services;
    }
}
