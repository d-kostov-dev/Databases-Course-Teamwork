namespace SexStore.MongoServer.Models
{
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Shop
    {
        [BsonConstructor]
        public Shop(string name, string address, ObjectId cityId, ICollection<ObjectId> productIds)
        {
            this.Name = name;
            this.Address = address;
            this.CityId = cityId;
            this.ProductIds = productIds;
        }

        [BsonId]
        public ObjectId Id { get; set; }

        // Unique
        [BsonRequired]
        public string Name { get; set; }

        [BsonRequired]
        public string Address { get; set; }

        [BsonRequired]
        public ObjectId CityId { get; set; }

        public City City { get; set; }

        public ICollection<ObjectId> ProductIds { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
