using Docentes.Domain.Abstractions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Docentes.Infrastructure.Converters;

public class NullableStronglyTypedIdConverter<TId> : ValueConverter<TId?, Guid?>
    where TId : notnull, IStronglyTypedId
{
    public NullableStronglyTypedIdConverter()
        : base(
            id => StronglyTypedIdConverterHelper.ConvertToNullableGuid(id),
            value => StronglyTypedIdConverterHelper.ConvertToNullableStronglyTypedId<TId>(value))
    {
    }
}