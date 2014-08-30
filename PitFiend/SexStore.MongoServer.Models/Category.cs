namespace SexStore.MongoServer.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Category
    {
        [BsonConstructor]
        public Category(string name)
        {
            this.Name = name;
        }

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        ////public ISet<Product> Products { get; set; }

        ////public void AddProduct(Product product)
        ////{
        ////    this.Products.Add(product);
        ////}

        ////public void RemoveProduct(Product product)
        ////{
        ////    this.Products.Remove(product);
        ////}
    }
}
