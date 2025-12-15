using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pedidos.Domain.Abstractions;
using Pedidos.Infrastructure.Repositories;
using Pedidos.Infrastructure.Services; // Add this using directive
using Pedidos.Application.Abstractions; // Add this using directive

namespace Pedidos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PedidosDb"))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IPedidoRepository, PedidoRepository>();
        
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        // Add HTTP Client for Cursos microservice
        services.AddHttpClient<ICursosHttpClient, CursosHttpClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["ApiSettings:CursosApiBaseUrl"] ?? throw new InvalidOperationException("ApiSettings:CursosApiBaseUrl is not configured."));
        });

        return services;
    }
}