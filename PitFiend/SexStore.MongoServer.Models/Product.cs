namespace SexStore.MongoServer.Models
{
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Product
    {
        [BsonConstructor]
        public Product(
            string name,
            string description,
            int productCode,
            decimal price,
            int unitsInStock,
            ObjectId typeId,
            ICollection<int> categoryIds)
        {
            this.Name = name;
            this.Description = description;
            this.ProductCode = productCode;
            this.Price = price;
            this.UnitsInStock = unitsInStock;
            this.TypeId = typeId;
            this.CategoryIds = categoryIds;
        }

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        public string Description { get; set; }

        // Unique
        [BsonRequired]
        public int ProductCode { get; set; }

        [BsonRequired]
        public decimal Price { get; set; }

        public int UnitsInStock { get; set; }

        [BsonRequired]
        public ObjectId TypeId { get; set; }

        public ProductType Type { get; set; }

        // MongoDB is detached here
        // These IDs are needed for establishing the relation with MS SQL
        public ICollection<int> CategoryIds { get; set; }
    }
}
