namespace SexStore.Client
{
    using System;
    using System.Data.Entity;
    using SQLServer.Data;
    using SQLiteServer.Data;
    using OpenAccessRuntime;
    using MySQLServer;

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

                Console.WriteLine();
                Console.WriteLine("Products Info From SQLite DB");
                Console.WriteLine("--------------------");

                //var sqLiteConnection =
                //    new SQLiteServConnection("Data Source=C:\\SQLite\\SexStoreProductInfo.sqlite;Version=3;");
                //var productsInfo = sqLiteConnection.GetProductsInformation();

                //foreach (var product in productsInfo)
                //{
                //    Console.WriteLine("Product Code: {0}, Name: {1}, Tax: {2}", product.ProductCode, product.ProductName, product.TaxPercent);
                //}
            }

            var mySQLConnection = new MySQLContext("MySQLConnStrGYaramov");

            using (mySQLConnection)
            {
                var newReport = new sexStoreReports() { Id = 3, product_code = 1001, product_name = "Miss Dulboko Gurlo" };
                mySQLConnection.Add(newReport);
                mySQLConnection.SaveChanges();
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
