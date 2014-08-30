namespace SexStore.MongoServer.Data.Transfers
{
    using System;
    using System.Linq;
    using MongoDB.Driver;
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

        public void TransferData()
        {
            this.Context.Shops.AddRange(this.Parsed.GetShops());
            this.Context.SaveChanges();

            Console.WriteLine("Data transferred");
        }
    }
}
