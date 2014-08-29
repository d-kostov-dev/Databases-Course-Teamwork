using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace SexStore.MongoDB.Client
{
    using System;
    using System.Linq;
    using MongoDB.Models;

    public class EntryPoint
    {
        public static void Main()
        {
            MongoClient mongoClient = new MongoClient(MongoDB.Client.Settings.Default.Connection);
            MongoServer mongoServer = mongoClient.GetServer();
            MongoDatabase sexStore = mongoServer.GetDatabase(MongoDB.Client.Settings.Default.Database);

            // ...
        }
    }
}
