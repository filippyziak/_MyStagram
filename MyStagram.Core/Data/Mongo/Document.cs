using MongoDB.Bson;

namespace MyStagram.Core.Data.Mongo
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }
    }
}