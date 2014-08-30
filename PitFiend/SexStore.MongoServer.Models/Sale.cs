namespace SexStore.MongoServer.Models
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Sale
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired]
        public int Quantity { get; set; }

        [BsonRequired]
        public DateTime SaleDate { get; set; }

        [BsonRequired]
        public ObjectId ProductId { get; set; }

        public Product Product { get; set; }

        [BsonRequired]
        public ObjectId ShopId { get; set; }

        public Shop Shop { get; set; }
    }
}
