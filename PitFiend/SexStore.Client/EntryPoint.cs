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
        //private static SQLServerContext sqlServerConnection;

        public static void Main()
        {
            Console.WriteLine("Welcome to World of Sex - Toys For Destruction");
            Console.WriteLine("Loading...");

            InitDatabasesMigrations();
            ShowMenu();

            while (true)
            {
                ExecuteCommands();
            }
            //sqlServerConnection = new SQLServerContextFactory().Create();
            //var mySQLConnection = new MySQLContext("MySQLConnStrDKostovLaptop");

            ////var reports = new List<ProductReport>();

            ////using (sqlServerConnection)
            ////{
            ////    var allCities = sqlServerConnection.Cities;

            ////    Console.WriteLine("Cities");
            ////    Console.WriteLine("--------------------");
            ////    foreach (var city in allCities)
            ////    {
            ////        Console.WriteLine(city.Name);
            ////    }

            ////    Console.WriteLine();
            ////    Console.WriteLine("Products");
            ////    Console.WriteLine("--------------------");

            ////    var products = sqlServerConnection.Products;

            ////    foreach (var product in products)
            ////    {
            ////        Console.WriteLine(product.Name);
            ////    }

            ////    Console.WriteLine();
            ////    Console.WriteLine("Shops");
            ////    Console.WriteLine("--------------------");

            ////    var shops = sqlServerConnection.Shops;

            ////    foreach (var shop in shops)
            ////    {
            ////        Console.WriteLine(shop.Name);
            ////    }

            ////    Console.WriteLine();
            ////    Console.WriteLine("Sales");
            ////    Console.WriteLine("--------------------");

            ////    var sales = sqlServerConnection.Sales.Where(x => x.Product.ProductCode == 1001);

            ////    foreach (var sale in sales)
            ////    {
            ////        Console.WriteLine("Sale product: {0}, Date:{1}, Quantity: {2}", sale.Product.Name, sale.SaleDate, sale.Quantity);
            ////    }

            ////    Console.WriteLine();
            ////    Console.WriteLine("Products Info From SQLite DB");
            ////    Console.WriteLine("--------------------");

            ////    //XLSXExporter.ExportXlsxReport(new SQLiteServConnection(@"Data Source=..\..\..\SQLiteServer.Data\SexStoreProductInfo.sqlite;Version=3;"));

            ////    InputCommand();
            ////}

            //sqlServerConnection = new SQLServerContextFactory().Create();
            //reports = ProductReportsCreator.CreateReportForEveryProduct(sqlServerConnection);

            //JsonReporter.ExportReportToJsonFiles(reports);


            ////MySQLReporter.ExportReportToMySQLDb(mySQLConnection, reports);
        }

        private static void InitDatabasesMigrations()
        {
            // SQL Server
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SQLServerContext, SQLServer.Data.Migrations.Configuration>());
        }

        private static void ShowMenu()
        {
            Console.WriteLine(MenuSeparator());
            Console.WriteLine("'SP' - Shows all products in the SQLServer Database");
            Console.WriteLine("'ZEX - Exports information from ZIP file. Extracts the Excel data and writes to MongoDB'");
            Console.WriteLine("'MIP' - Imports products from MongoDB to SQLServer");

            Console.WriteLine();
            Console.WriteLine("PDF Operations:");

            Console.WriteLine("'QPDF' - Generates PDF report for 'Available Quantities'");
            Console.WriteLine("'SPDF' - Generates PDF report for 'Sales'");

            Console.WriteLine();
            Console.WriteLine("XML Operations:");

            Console.WriteLine("'QXML' - Generates XML report for 'Available Quantities'");
            Console.WriteLine("'SXML' - Generates XML report for 'Sales'");
            Console.WriteLine("'XMLS' - Imports data from XML to SQLServer");
            Console.WriteLine("'XMLM' - Imports data from XML to MongoDB");

            Console.WriteLine();

            Console.WriteLine("'JSR' - Generates JSON report for 'SALES' and saves the information in MySQL");
            Console.WriteLine("'GEX' - Generates excel report from MySQL and SQLite");

            Console.WriteLine(MenuSeparator());
        }

        private static string MenuSeparator(int separatorLength = 30)
        {
            return new string('-', separatorLength);
        }

        private static void ExecuteCommands()
        {
            string command = Console.ReadLine().ToLowerInvariant();

            if (command == "sp")
            {
                Console.Clear();
                Console.WriteLine("Showing all products in the SQL Server database...");
                // TO DO:
            }
            else if (command == "zex")
            {
                Console.Clear();
                Console.WriteLine("Exporting information from ZIP file...");

                Console.Write("Enter file path:");
                var filePath = Console.ReadLine();
                // TO DO:

                Console.Clear();

                // IF TRUE
                Console.WriteLine("Data from ZIP imported");
            }
            else if (command == "mip")
            {
                Console.Clear();
                Console.WriteLine("Transfering products data from MongoDB to SQL Server...");
                // TO DO:

                Console.Clear();

                // IF TRUE
                Console.WriteLine("Data Transfered");
            }
            else if (command == "qpdf")
            {
                Console.Clear();
                Console.WriteLine("Generating PDF report for 'Available Quantities'...");
                // TO DO:

                Console.Clear();

                // IF TRUE
                Console.WriteLine("Report Generated");
            }
            else if (command == "spdf")
            {
                Console.Clear();
                Console.WriteLine("Generating PDF report for 'Sales'...");
                // TO DO:

                Console.Clear();

                // IF TRUE
                Console.WriteLine("Report Generated");
            }
            else if (command == "qxml")
            {
                Console.Clear();
                Console.WriteLine("Generating XML report for 'Available Quantities'...");
                // TO DO:

                Console.Clear();

                // IF TRUE
                Console.WriteLine("Report Generated");
            }
            else if (command == "sxml")
            {
                Console.Clear();
                Console.WriteLine("Generating XML report for 'Sales'...");
                // TO DO:

                Console.Clear();

                // IF TRUE
                Console.WriteLine("Report Generated");
            }
            else if (command == "xmls")
            {
                Console.Clear();
                Console.WriteLine("Importing data from XML to SQL Server...");

                Console.Write("Enter file path:");
                var filePath = Console.ReadLine();
                // TO DO:

                Console.Clear();

                // IF TRUE
                Console.WriteLine("Import Completed");
            }
            else if (command == "xmlm")
            {
                Console.Clear();
                Console.WriteLine("Importing data from XML to MongoDB...");

                Console.Write("Enter file path:");
                var filePath = Console.ReadLine();
                // TO DO:

                Console.Clear();

                // IF TRUE
                Console.WriteLine("Import Completed");
            }
            else if (command == "jsr")
            {
                Console.Clear();
                Console.WriteLine("Generating JSON report for 'SALES'");
                // TO DO:

                Console.Clear();

                Console.WriteLine("Saving JSON reports in MySQL...");
                // TO DO:

                Console.Clear();

                // IF TRUE
                Console.WriteLine("Operation Completed");
            }
            else if (command == "gex")
            {
                Console.Clear();
                Console.WriteLine("Generating Excel report...");
                // TO DO:

                Console.Clear();

                // IF TRUE
                Console.WriteLine("Report Generated");
            }

            Console.WriteLine();
            ShowMenu();
        }
    }
}
