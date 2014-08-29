using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SexStore.MongoDB.Models
{
    public class Shop
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        [BsonRequired]
        public string Address { get; set; }

        [BsonRequired]
        public ObjectId CityId { get; set; }
        public City City { get; set; }

        [BsonConstructor]
        public Shop(string name, string address, ObjectId cityId)
        {
            this.Name = name;
            this.Address = address;
            this.CityId = cityId;
        }
    }
}
