namespace SexStore.MongoServer.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class ProductCategory
    {
        [BsonConstructor]
        public ProductCategory(ObjectId productId, ObjectId categoryId)
        {
            this.ProductId = productId;
            this.CategoryId = categoryId;
        }

        [BsonRequired]
        public ObjectId ProductId { get; set; }

        public Product Product { get; set; }

        [BsonRequired]
        public ObjectId CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
