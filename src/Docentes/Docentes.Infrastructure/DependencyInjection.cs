using Docentes.Application.Services;
using Docentes.Domain.Abstractions;
using Docentes.Domain.CursosImpartidos;
using Docentes.Domain.Docentes;
using Docentes.Domain.Especialidades;
using Docentes.Infrastructure.Repositories;
using Docentes.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
namespace Docentes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration configuration
   )
    {
        var connectionStringPostgres = configuration.GetConnectionString("DocentesDb")
        ?? throw new ArgumentNullException(nameof(configuration));

        var connectionStringRedis = configuration.GetConnectionString("RedisDb")
        ?? throw new ArgumentNullException(nameof(configuration));

        var usuarioApiBaseUrl = configuration["ApiSettings:UsuariosApiBaseUrl"];
        var cursoApiBaseUrl = configuration["ApiSettings:CursosApiBaseUrl"];

        
        services.AddDbContext<ApplicationDbContext>(
            options =>
            {
                options.UseNpgsql(connectionStringPostgres).UseSnakeCaseNamingConvention();
            }
        );

        services.AddSingleton<IConnectionMultiplexer>( sp =>
        {
            var configurationRedis = ConfigurationOptions.Parse(connectionStringRedis);
            return ConnectionMultiplexer.Connect(configurationRedis);
        });

        services.AddScoped<IDocenteRepository, DocenteRepository>();
        services.AddScoped<ICursoImpartidoRepository, CursoImpartidoRepository>();
        services.AddScoped<IEspecialidadRepository, EspecialidadRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ICacheService,CacheService>();

        services.AddHttpClient<ICursosService,CursosService>( cliente =>
        {
            cliente.BaseAddress = new Uri(cursoApiBaseUrl!);
        });

          services.AddHttpClient<IUsuarioService,UsuarioService>( cliente =>
        {
            cliente.BaseAddress = new Uri(usuarioApiBaseUrl!);
        });

        return services;
    }
}