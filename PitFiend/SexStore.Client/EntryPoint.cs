namespace SexStore.Client
{
    using System;
    using System.Data.Entity;
    using SQLServer.Data;
    using SQLiteServer.Data;
    using OpenAccessRuntime;
    using MySQLServer;
    using System.Linq;

    using SexStore.Client.Readers;
    using SexStore.Client.Readers.Reporters;
    using System.Collections.Generic;
    using SexStore.Models;

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
                //Console.WriteLine("Cities");
                //Console.WriteLine("--------------------");
                //var allCities = sqlServerConnection.Cities;

                //foreach (var city in allCities)
                //{
                //    Console.WriteLine(city.Name);
                //}

                //Console.WriteLine();
                //Console.WriteLine("Products");
                //Console.WriteLine("--------------------");

                //var products = sqlServerConnection.Products;

                //foreach (var product in products)
                //{
                //    Console.WriteLine(product.Name);
                //}

                //Console.WriteLine();
                //Console.WriteLine("Shops");
                //Console.WriteLine("--------------------");

                //var shops = sqlServerConnection.Shops;

                //foreach (var shop in shops)
                //{
                //    Console.WriteLine(shop.Name);
                //}

                //Console.WriteLine();
                //Console.WriteLine("Sales");
                //Console.WriteLine("--------------------");

                //var sales = sqlServerConnection.Sales.Where(x => x.Product.ProductCode == 1001);
                

                //foreach (var sale in sales)
                //{
                //    Console.WriteLine("Sale product: {0}, Date:{1}, Quantity: {2}", sale.Product.Name, sale.SaleDate, sale.Quantity);
                //}

                //Console.WriteLine();
                //Console.WriteLine("Products Info From SQLite DB");
                //Console.WriteLine("--------------------");

                //XLSXExporter.ExportXlsxReport(new SQLiteServConnection(@"Data Source=..\..\..\SQLiteServer.Data\SexStoreProductInfo.sqlite;Version=3;"));
                //XMLExporter.ExportRemainingQuantitiesToXml(sqlServerConnection);
                //PDFExporter.ExportRemainingQuantitiesToPdf(sqlServerConnection);

                //var sale = sqlServerConnection.Sales.Find(6);
                //sale.Quantity = 2;

                //sqlServerConnection.SaveChanges();

                //ProductReport.ExportToJson(sqlServerConnection);

                reports = ProductReport.CreateReportForEveryProduct(sqlServerConnection);
            }



            var mySQLConnection = new MySQLContext("MySQLConnStrGYaramov");
            
            using (mySQLConnection)
            {
                foreach (var report in reports)
                {
                    var newReport = new sexStoreReports() 
                    { 
                        Id = report.Id, 
                        ProductCode = report.ProductCode, 
                        Name = report.Name,
                        SoldInShops = string.Join(" ", report.ShopNames),
                        TotalQuantitySold = report.TotalQuantitySold,
                        TotalIncomes = report.TotalIncomes
                    };
                    mySQLConnection.Add(newReport);
                    
                }

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
