using Evaluaciones.Domain.Evaluaciones;
using Evaluaciones.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Evaluaciones.Application.Services;
using Evaluaciones.Infrastructure.Services;

namespace Evaluaciones.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("EvaluacionesDb")));
        services.AddScoped<IEvaluacionRepository, EvaluacionRepository>();
        
        services.AddHttpClient<IEstudiantesService, EstudiantesService>(client =>
        {
            client.BaseAddress = new Uri(configuration["ApiSettings:EstudiantesApiBaseUrl"] ?? "http://localhost:5003/api/");
        });

        return services;
    }
}