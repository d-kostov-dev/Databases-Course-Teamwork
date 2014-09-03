namespace SexStore.MongoServer.Data.Transfers
{
    using System;
    using System.Linq;
    using MongoDB.Driver;
    using SexStore.Models;
    using SQLServer.Data;

    public class TransferEngine
    {
        public TransferEngine(MongoDatabase database)
        {
            this.Context = new SQLServerContextFactory().Create();
            this.Parsed = new SqlParser(database, this.Context);
            this.Parsed.InitializeParsing();
        }

        private SQLServerContext Context { get; set; }

        private SqlParser Parsed { get; set; }

        public int TransferData()
        {
            foreach (Shop shop in this.Parsed.Shops)
            {
                try
                {
                    this.Context.Shops.Add(shop);
                    this.Context.SaveChanges();
                }
                catch
                {
                    continue;
                }
            }
            
            //int results = this.Context.SaveChanges();
            int results = 1;

            return results;
        }
    }
}
