namespace SexStore.MongoServer.Client
{
    using System;
    using System.Linq;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoServer.Data.Migrations;
    using MongoServer.Models;
        
    public class EntryPoint
    {
        public static void Main()
        {
            MongoClient mongoClient = new MongoClient(SexStore.MongoServer.Client.Settings.Default.Connection);
            MongoServer mongoServer = mongoClient.GetServer();
            MongoDatabase sexStore = mongoServer.GetDatabase(SexStore.MongoServer.Client.Settings.Default.Database);

            //// ...

            Seed test = new Seed(sexStore);
            test.Initialize();
        }
    }
}
