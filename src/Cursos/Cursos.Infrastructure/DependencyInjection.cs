using Cursos.Application.Services;
using Cursos.Domain.Cursos;
using Cursos.Infrastructure.Repositories;
using Cursos.Infrastructure.Serializers;
using Cursos.Infrastructure.services;
using Cursos.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Serilog;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;

namespace Cursos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration["MongoDbSettings:ConnectionString"] ??
            throw new ArgumentNullException("MongoDbSettings:ConnectionString not found in configuration.");
        var databaseName = configuration["MongoDbSettings:DatabaseName"] ??
            throw new ArgumentNullException("MongoDbSettings:DatabaseName not found in configuration.");

        var graylogHost = configuration["Graylog:Host"];
        var graylogPort = configuration.GetValue<int>("Graylog:Port");

        services.AddSingleton<IMongoClient, MongoClient>(sp =>
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
            return database.GetCollection<Curso>("cursos");
        });


        services.AddScoped<ICursoRepository, CursoRepository>();

        BsonSerializer.RegisterSerializer(new CapacidadCursoSerializer());
        BsonSerializer.RegisterSerializer(new DescripcionCursoSerializer());
        BsonSerializer.RegisterSerializer(new NombreCursoSerializer());

        services.AddSingleton<IEventBus,RabbitMQEventBus>();
        services.AddHostedService<RabbitMQEventListener>();

        var logger = new LoggerConfiguration()
           .MinimumLevel.Information()
           .Enrich.FromLogContext()
           .WriteTo.Graylog(new GraylogSinkOptions
           {
               HostnameOrAddress = graylogHost,
               Port = graylogPort,
               TransportType = TransportType.Udp,
               Facility = "CursosService"
           })
           .CreateLogger();
        
        Log.Logger = logger;

        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose:true));

        return services;
    }
}