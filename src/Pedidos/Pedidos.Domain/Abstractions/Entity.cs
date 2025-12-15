namespace Pedidos.Domain.Abstractions;

public abstract class Entity<TId>
{
    protected Entity()
    {
        Id = default!;
    }

    protected Entity(TId id)
    {
        Id = id;
    }

    public TId Id { get; init; }
}