namespace SexStore.MongoDb.Client
{
    using System;
    using System.Linq;
    using MongoDb.Data.Migrations;
    using MongoDb.Models;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
        
    public class EntryPoint
    {
        public static void Main()
        {
            MongoClient mongoClient = new MongoClient(MongoDb.Client.Settings.Default.Connection);
            MongoServer mongoServer = mongoClient.GetServer();
            MongoDatabase sexStore = mongoServer.GetDatabase(MongoDb.Client.Settings.Default.Database);

            //// ...

            Seed test = new Seed(sexStore);
            test.Initialize();
        }
    }
}
