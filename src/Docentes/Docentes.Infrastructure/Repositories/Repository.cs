using Docentes.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Docentes.Infrastructure.Repositories;

internal abstract class Repository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : IStronglyTypedId
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<TEntity>()
            .FirstOrDefaultAsync(entity => entity.Id.Equals(id), cancellationToken);
    }

    public void Add(TEntity entity)
    {
        DbContext.Add(entity);
    }
}
