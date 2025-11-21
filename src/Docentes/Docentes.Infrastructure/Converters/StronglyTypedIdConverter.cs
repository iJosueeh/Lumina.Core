using Docentes.Domain.Abstractions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Docentes.Infrastructure.Converters;

public class StronglyTypedIdConverter<TId> : ValueConverter<TId, Guid> where TId : notnull, IStronglyTypedId
{
    public StronglyTypedIdConverter()
        : base(
            id => StronglyTypedIdConverterHelper.ConvertToGuid(id),
            value => StronglyTypedIdConverterHelper.ConvertToStronglyTypedId<TId>(value))
    {
    }
}