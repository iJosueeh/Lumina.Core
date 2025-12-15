using Estudiantes.Domain.Estudiantes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Estudiantes.Infrastructure;

public static class DataSeeder
{
    public static void Seed(IHost app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        // Ensure the database is created
        dbContext.Database.Migrate();

        // Clear the carritos table for development purposes
        var carritos = dbContext.Set<Carrito>();
        if (carritos.Any())
        {
            carritos.RemoveRange(carritos);
            dbContext.SaveChanges();
        }
    }
}
