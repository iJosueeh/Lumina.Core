namespace Evaluaciones.Domain.Abstractions;

public abstract class Entity(Guid id)
{
    public Guid Id { get; protected set; } = id;
}