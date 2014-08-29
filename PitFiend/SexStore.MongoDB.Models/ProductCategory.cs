using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SexStore.MongoDB.Models
{
    public class ProductCategory
    {
        [BsonRequired]
        public ObjectId ProductId { get; set; }
        public Product Product { get; set; }

        [BsonRequired]
        public ObjectId CategoryId { get; set; }
        public Category Category { get; set; }

        [BsonConstructor]
        public ProductCategory(ObjectId productId, ObjectId categoryId)
        {
            this.ProductId = productId;
            this.CategoryId = categoryId;
        }
    }
}
