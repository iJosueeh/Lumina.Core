using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Estudiantes.Application.Abstractions.Clock;
using Estudiantes.Domain.Abstractions;
using Estudiantes.Domain.Estudiantes;
using Estudiantes.Infrastructure.Clock;
using Estudiantes.Infrastructure.Repositories;
using Estudiantes.Domain.Matriculas;
using StackExchange.Redis;
using Estudiantes.Application.Services;
using Estudiantes.Infrastructure.Services;
using Estudiantes.Domain.Programaciones;
using Estudiantes.Infrastructure.services;
using Serilog;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;
using Npgsql;

namespace Estudiantes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        var connectionStringPostgres = configuration.GetConnectionString("EstudiantesDb")
        ?? throw new ArgumentNullException(nameof(configuration));

        var connectionStringRedis = configuration.GetConnectionString("RedisDb")
        ?? throw new ArgumentNullException(nameof(configuration));

        var rabbitMQHost = configuration["RabbitMQSettings:Host"]
        ?? throw new ArgumentNullException(nameof(configuration));

        var usuariosApiBaseUrl = configuration["ApiSettings:UsuariosApiBaseUrl"];
        var cursosApiBaseUrl = configuration["ApiSettings:CursosApiBaseUrl"];
        var docentesApiBaseUrl = configuration["ApiSettings:DocentesApiBaseUrl"];

        var graylogHost = configuration["Graylog:Host"];
        var graylogPort = configuration.GetValue<int>("Graylog:Port");

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionStringPostgres);
        dataSourceBuilder.EnableDynamicJson();
        var dataSource = dataSourceBuilder.Build();

        services.AddDbContext<ApplicationDbContext>(
            options =>
            {
                options.UseNpgsql(dataSource).UseSnakeCaseNamingConvention(); 
            }
        );

        services.AddSingleton<IConnectionMultiplexer>(sp =>
       {
           var configurationRedis = ConfigurationOptions.Parse(connectionStringRedis);
           return ConnectionMultiplexer.Connect(configurationRedis);
       });

        services.AddScoped<IEstudianteRepository, EstudianteRepository>();
        services.AddScoped<IMatriculaRepository, MatriculaRepository>();
        services.AddScoped<IProgramacionRepository, ProgramacionRepository>();
        services.AddScoped<IInscripcionRepository, InscripcionRepository>();
        services.AddScoped<ICarritoRepository, CarritoRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddHttpClient<IUsuariosService, UsuarioService>(client =>
       {
           client.BaseAddress = new Uri(usuariosApiBaseUrl!);
       });

        services.AddHttpClient<ICursosService, CursoService>(client =>
        {
            client.BaseAddress = new Uri(cursosApiBaseUrl!);
        });

        services.AddHttpClient<IDocentesService, DocenteService>(client =>
        {
            client.BaseAddress = new Uri(docentesApiBaseUrl!);
        });

        services.AddScoped<ICacheService, RedisCacheService>();

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
               Facility = "EstudianteService"
           })
           .CreateLogger();
        
        Log.Logger = logger;

        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose:true));

        return services;
    }

}
