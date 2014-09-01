namespace SexStore.Client
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using MySQLServer;
    using OpenAccessRuntime;
    using SexStore.Client.Readers;
    using SexStore.Client.Readers.Helpers;
    using SexStore.Client.Readers.Reporters;
    using SexStore.Models;
    using SQLiteServer.Data;
    using SQLServer.Data;
    
    public class EntryPoint
    {
        private static SQLServerContext sqlServerConnection;

        public static void Main()
        {
            InitDatabasesMigrations();

            sqlServerConnection = new SQLServerContextFactory().Create();

            var reports = new List<ProductReport>();

            using (sqlServerConnection)
            {
                var allCities = sqlServerConnection.Cities;

                Console.WriteLine("Cities");
                Console.WriteLine("--------------------");
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

                var sales = sqlServerConnection.Sales.Where(x => x.Product.ProductCode == 1001);
                
                foreach (var sale in sales)
                {
                    Console.WriteLine("Sale product: {0}, Date:{1}, Quantity: {2}", sale.Product.Name, sale.SaleDate, sale.Quantity);
                }

                Console.WriteLine();
                Console.WriteLine("Products Info From SQLite DB");
                Console.WriteLine("--------------------");

                //XLSXExporter.ExportXlsxReport(new SQLiteServConnection(@"Data Source=..\..\..\SQLiteServer.Data\SexStoreProductInfo.sqlite;Version=3;"));

                InputCommand();
            }

            sqlServerConnection = new SQLServerContextFactory().Create();
            reports = ProductReportsCreator.CreateReportForEveryProduct(sqlServerConnection);

            JsonReporter.ExportReportToJsonFiles(reports);

            //var mySQLConnection = new MySQLContext("MySQLConnStrGYaramov");
            //MySQLReporter.ExportReportToMySQLDb(mySQLConnection, reports);
        }

        private static void InitDatabasesMigrations()
        {
            // SQL Server
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SQLServerContext, SQLServer.Data.Migrations.Configuration>());
        }

        private static void InputCommand()
        {
            Console.WriteLine("Press 1 to generate XML report.");
            Console.WriteLine("Press 2 to generate PDF report.");
            var command = Console.ReadLine().ToLower();
            switch (command)
            {
                case "1": XMLExporter.RemainingQuantities(sqlServerConnection); InputCommand(); break;
                case "2": PDFExporter.RemainingQuantities(sqlServerConnection); InputCommand(); break;
                default: Console.WriteLine("Invalid Command"); InputCommand(); break;
            }
            
            
        }
    }
}
