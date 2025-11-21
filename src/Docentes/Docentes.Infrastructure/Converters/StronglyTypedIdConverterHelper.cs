using Docentes.Domain.Abstractions;
using System;

namespace Docentes.Infrastructure.Converters;

public static class StronglyTypedIdConverterHelper
{
    public static Guid ConvertToGuid<TId>(TId id) where TId : notnull, IStronglyTypedId
    {
        return id.Value;
    }

    public static TId ConvertToStronglyTypedId<TId>(Guid value) where TId : notnull, IStronglyTypedId
    {
        return (TId)Activator.CreateInstance(typeof(TId), value)!;
    }

    // New methods for nullable types
    public static Guid? ConvertToNullableGuid<TId>(TId? id) where TId : notnull, IStronglyTypedId
    {
        return id?.Value; // Use null-conditional operator
    }

    public static TId? ConvertToNullableStronglyTypedId<TId>(Guid? value) where TId : notnull, IStronglyTypedId
    {
        return value == null ? (TId?)default(TId) : (TId)Activator.CreateInstance(typeof(TId), value)!;
    }
}