using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SexStore.MongoDB.Models
{
    //using System.Collections.Generic;

    public class Category
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        //public ISet<Product> Products { get; set; }

        [BsonConstructor]
        public Category(string name)
        {
            this.Name = name;
        }

        //public void AddProduct(Product product)
        //{
        //    this.Products.Add(product);
        //}

        //public void RemoveProduct(Product product)
        //{
        //    this.Products.Remove(product);
        //}
    }
}
