namespace SexStore.Client
{
    using System;
    using System.Data.Entity;
    using SQLServer.Data;
    using SQLServer.Data.Migrations;

    public class EntryPoint
    {
        public static void Main()
        {
            Database.SetInitializer
                (new MigrateDatabaseToLatestVersion<SQLServerContext, Configuration>());

            
            var dbConnection = new SQLServerContextFactory().Create();

            using (dbConnection)
            {
                var allCities = dbConnection.Cities;

                foreach (var city in allCities)
                {
                    Console.WriteLine(city.Name);
                }
            }
        }
    }
}
