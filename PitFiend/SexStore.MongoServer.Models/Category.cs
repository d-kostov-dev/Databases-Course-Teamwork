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
    }
}
