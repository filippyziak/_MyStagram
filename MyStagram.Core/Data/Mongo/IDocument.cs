using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyStagram.Core.Data.Mongo
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        ObjectId Id { get; set; }
    }
}