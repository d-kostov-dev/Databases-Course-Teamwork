namespace SexStore.Client
{
    using System;
    using System.Data.Entity;
    using SQLServer.Data;
    using MySQLServer.Data;

    public class EntryPoint
    {
        public static void Main()
        {
            InitDatabasesMigrations();

            
            var dbConnection = new MySQLContextFactory().Create();

            using (dbConnection)
            {
                var allCities = dbConnection.Cities;

                foreach (var city in allCities)
                {
                    Console.WriteLine(city.Name);
                }
            }
        }

        private static void InitDatabasesMigrations()
        {
            // SQL Server
            Database.SetInitializer
                (new MigrateDatabaseToLatestVersion<SQLServerContext, SQLServer.Data.Migrations.Configuration>());

            // MySQL
            Database.SetInitializer
                (new MigrateDatabaseToLatestVersion<MySQLContext, MySQLServer.Data.Migrations.Configuration>());
        }
    }
}
