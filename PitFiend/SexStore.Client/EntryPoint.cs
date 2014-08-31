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

    public class EntryPoint
    {
        private static SQLServerContext sqlServerConnection;

        public static void Main()
        {
            InitDatabasesMigrations();

            sqlServerConnection = new SQLServerContextFactory().Create();

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


                //var sales = sqlServerConnection.Shops.SelectMany(s => s.Sales).AsQueryable();
                //var sellPro = sales.Select(s => s.Product);
                //Console.WriteLine(sales);
                //Console.WriteLine();
                //Console.WriteLine(sellPro);

                //foreach (var prod in sellPro)
                //{
                //    Console.WriteLine(prod.Name);
                //}
                //sales.


                var allProducts = sqlServerConnection.Products;
                var productReports = new List<ProductReport>();

                foreach (var p in allProducts)
                {
                    var prodRep = new ProductReport();
                    prodRep.Id = p.ID;
                    prodRep.Name = p.Name;
                    prodRep.TotalQuantitySold = sqlServerConnection.Sales
                        .Where(s => s.Product.ID == p.ID)
                            .Sum(s => s.Quantity);
                    prodRep.TotalIncomes = (decimal)prodRep.TotalQuantitySold * (decimal)p.Price;
                    productReports.Add(prodRep);
                }

                foreach (var rep in productReports)
                {
                    Console.WriteLine(rep.Id);
                    Console.WriteLine(rep.Name);
                    Console.WriteLine(rep.TotalQuantitySold);
                    Console.WriteLine(rep.TotalIncomes);
                }
            }

            //var mySQLConnection = new MySQLContext("MySQLConnStrGYaramov");
            
            //using (mySQLConnection)
            //{
            //    var newReport = new sexStoreReports() { Id = 3, product_code = 1001, product_name = "Miss Dulboko Gurlo" };
            //    mySQLConnection.Add(newReport);
            //    mySQLConnection.SaveChanges();
            //}
        }


        private static void InitDatabasesMigrations()
        {
            // SQL Server
            Database.SetInitializer
                (new MigrateDatabaseToLatestVersion<SQLServerContext, SQLServer.Data.Migrations.Configuration>());
        }
    }
}
