namespace SexStore.MongoServer.Client
{
    using System;
    using System.Linq;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoServer.Data.Migrations;
    using MongoServer.Data.Transfers;
    using MongoServer.Models;
        
    public class EntryPoint
    {
        public static void Main()
        {
            MongoClient mongoClient = new MongoClient(Settings.Default.Connection);
            MongoServer mongoServer = mongoClient.GetServer();
            MongoDatabase sexStore = mongoServer.GetDatabase(Settings.Default.Database);

            //// ...

            Seed test = new Seed(sexStore);
            test.Initialize();

            MongoCollection<BsonDocument> cities = sexStore.GetCollection("Cities");

            ////City qmbol = new City("Qmbol");
            ////cities.Insert(qmbol);

            TransferEngine yolo = new TransferEngine(sexStore);
            yolo.TransferData();
        }
    }
}
