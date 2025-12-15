using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NoticiasEventos.Domain.Abstractions;

public abstract class Entity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; protected set; } 

    protected Entity()
    {
        Id = Guid.NewGuid();
    }
}
