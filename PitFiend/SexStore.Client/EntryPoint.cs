namespace SexStore.Client
{
    using System;
    using System.Data.Entity;
    using SQLServer.Data;

    public class EntryPoint
    {
        private static SQLServerContext sqlServerConnection;

        public static void Main()
        {
            InitDatabasesMigrations();

            sqlServerConnection = new SQLServerContextFactory().Create();

            using (sqlServerConnection)
            {
                Console.WriteLine("Cities");
                Console.WriteLine("--------------------");
                var allCities = sqlServerConnection.Cities;

                foreach (var city in allCities)
                {
                    Console.WriteLine(city.Name);
                }

                Console.WriteLine();
                Console.WriteLine("Products");
                Console.WriteLine("--------------------");

                var products = sqlServerConnection.Products;

                foreach (var product in products)
                {
                    Console.WriteLine(product.Name);
                }

                Console.WriteLine();
                Console.WriteLine("Shops");
                Console.WriteLine("--------------------");

                var shops = sqlServerConnection.Shops;

                foreach (var shop in shops)
                {
                    Console.WriteLine(shop.Name);
                }

                Console.WriteLine();
                Console.WriteLine("Sales");
                Console.WriteLine("--------------------");

                var sales = sqlServerConnection.Sales;

                foreach (var sale in sales)
                {
                    Console.WriteLine("Sale product: {0}, Date:{1}, Quantity: {2}", sale.Product.Name, sale.SaleDate, sale.Quantity);
                }
            }
        }

        private static void InitDatabasesMigrations()
        {
            // SQL Server
            Database.SetInitializer
                (new MigrateDatabaseToLatestVersion<SQLServerContext, SQLServer.Data.Migrations.Configuration>());
        }
    }
}
