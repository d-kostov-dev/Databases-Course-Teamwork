using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SexStore.MongoDB.Models
{
    public class ProductType
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        [BsonConstructor]
        public ProductType(string name)
        {
            this.Name = name;
        }
    }
}
