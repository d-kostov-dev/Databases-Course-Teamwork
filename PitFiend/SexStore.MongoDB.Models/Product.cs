using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SexStore.MongoDB.Models
{
    //using System.Collections.Generic;

    public class Product
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        public string Description { get; set; }

        [BsonRequired]
        public decimal Price { get; set; }

        public int UnitsInStock { get; set; }

        [BsonRequired]
        public ObjectId TypeId { get; set; }
        public ProductType Type { get; set; }

        [BsonRequired]
        public ObjectId ShopId { get; set; }
        public Shop Shop { get; set; }

        //public ISet<Category> Categories { get; set; }

        [BsonConstructor]
        public Product(string name, string description, decimal price, int unitsInStock, ObjectId typeId, ObjectId shopId)
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.UnitsInStock = unitsInStock;
            this.TypeId = typeId;
            this.ShopId = shopId;
        }

        //public void AddCategory(Category category)
        //{
        //    this.Categories.Add(category);
        //}

        //public void RemoveCategory(Category category)
        //{
        //    this.Categories.Remove(category);
        //}
    }
}
