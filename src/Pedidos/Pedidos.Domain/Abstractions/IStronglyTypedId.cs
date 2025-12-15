namespace Pedidos.Domain.Abstractions;

public interface IStronglyTypedId
{
    Guid Value { get; }
}