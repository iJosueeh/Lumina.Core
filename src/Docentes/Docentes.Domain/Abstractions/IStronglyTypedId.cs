namespace Docentes.Domain.Abstractions;

public interface IStronglyTypedId
{
    Guid Value { get; }
}