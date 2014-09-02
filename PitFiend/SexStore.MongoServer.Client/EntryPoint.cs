namespace SexStore.MongoServer.Client
{
    using System;
    using System.Linq;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoServer.Data.Imports;
    using MongoServer.Data.Initialization;
    using MongoServer.Data.Transfers;
    using MongoServer.Models;
        
    public class EntryPoint
    {
        public static void Main()
        {
            MongoClient mongoClient = new MongoClient("mongodb://localhost/");
            MongoServer mongoServer = mongoClient.GetServer();
            MongoDatabase sexStore = mongoServer.GetDatabase("SexStore");

            //// ...

            Seed test = new Seed(sexStore);
            test.Initialize();

            //////IMPORTING XML => MongoDB
            ////MongoProductImporter xmlImport = new MongoProductImporter(ImportType.XML, "NewProducts.xml", sexStore);
            ////xmlImport.InitializeImport();

            //////TRANSFERING MongoDB => SQL Server
            ////TransferEngine yolo = new TransferEngine(sexStore);
            ////yolo.TransferData();
        }
    }
}
