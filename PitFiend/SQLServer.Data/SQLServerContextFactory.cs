namespace SQLServer.Data
{
    using System.Data.Entity.Infrastructure;

    public class SQLServerContextFactory : IDbContextFactory<SQLServerContext>
    {
        public SQLServerContext Create()
        {
            // Check App.config and add your connection strings.
            return new SQLServerContext("SQLKrischoLaptop");
        }
    }
}
