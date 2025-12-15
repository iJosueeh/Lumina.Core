using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using NoticiasEventos.Domain.Eventos;
using NoticiasEventos.Domain.Noticias;
using NoticiasEventos.Infrastructure.Repositories;

namespace NoticiasEventos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration["MongoDbSettings:ConnectionString"] ??
            throw new ArgumentNullException("MongoDbSettings:ConnectionString not found in configuration.");
        var databaseName = configuration["NoticiasEventosDatabase"] ??
            throw new ArgumentNullException("NoticiasEventosDatabase not found in configuration.");

        services.AddSingleton<IMongoClient>(sp =>
        {
            return new MongoClient(connectionString);
        });

        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(databaseName);
        });

        services.AddScoped<INoticiaRepository, NoticiaRepository>();
        services.AddScoped<IEventoRepository, EventoRepository>();
        
        services.AddScoped<DataSeeder>();

        return services;
    }
}
