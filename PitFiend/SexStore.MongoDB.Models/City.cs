using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SexStore.MongoDB.Models
{
    public class City
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        [BsonConstructor]
        public City(string name)
        {
            this.Name = name;
        }
    }
}
