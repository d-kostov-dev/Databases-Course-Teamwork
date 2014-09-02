namespace SexStore.MongoServer.Client
{
    using System;
    using System.Linq;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoServer.Data.Imports;
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

            //// IMPORTING XML => MongoDB
            ////MongoProductImporter xmlImport = new MongoProductImporter(ImportType.XML, "NewProducts.xml", sexStore);
            ////xmlImport.InitializeImport();

            //// TRANSFERING MongoDB => SQL Server
            ////TransferEngine yolo = new TransferEngine(sexStore);
            ////yolo.TransferData();
        }
    }
}
