using Docentes.Application.Exceptions;
using Docentes.Domain.Abstractions;
using Docentes.Domain.Docentes;
using Docentes.Domain.Especialidades;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Docentes.Infrastructure;

public class ApplicationDbContext(DbContextOptions options, IPublisher publisher) : DbContext(options), IUnitOfWork
{
    public readonly IPublisher _publisher = publisher;
    public DbSet<Docente>? Docentes { get; set; }
    public DbSet<Especialidad>? Especialidades { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("La excepcion por concurrencia se disparo", ex);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}