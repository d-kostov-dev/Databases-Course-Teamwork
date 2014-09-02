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
    using SexStore.MongoServer.Data.Transfers;
    using MongoDB.Driver;
    using SexStore.MongoServer.Data.Imports;
    using SexStore.MongoServer.Data.Initialization;

    public class EntryPoint
    {
        public static void Main()
        {
            Console.WriteLine("Welcome to World of Sex - Toys For Destruction");
            Console.WriteLine("Loading...");

            InitDatabasesMigrations();
            SeedMongoDatabase();
            ShowMenu();

            while (true)
            {
                ExecuteCommands();
            }


            //var mySQLConnection = new MySQLContext("MySQLConnStrDKostovLaptop");

            ////var reports = new List<ProductReport>();

            ////using (sqlServerConnection)
            ////{

            ////    XLSXExporter.ExportXlsxReport(new SQLiteServConnection(@"Data Source=..\..\..\SQLiteServer.Data\SexStoreProductInfo.sqlite;Version=3;"));

            ////}

            //sqlServerConnection = new SQLServerContextFactory().Create();
            //reports = ProductReportsCreator.CreateReportForEveryProduct(sqlServerConnection);

            //JsonReporter.ExportReportToJsonFiles(reports);

            ////MySQLReporter.ExportReportToMySQLDb(mySQLConnection, reports);
        }

        /// <summary>
        /// Initializes the SQL Server database with migrations. New changes to the model, are automatically inserted in the database.
        /// </summary>
        private static void InitDatabasesMigrations()
        {
            // SQL Server
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SQLServerContext, SQLServer.Data.Migrations.Configuration>());
        }

        /// <summary>
        /// Seeds (inserts) initial documents into MongoDB
        /// </summary>
        private static void SeedMongoDatabase()
        {
            // MongoDB
            MongoServer mongoServer = new MongoClient(Settings.Default.MongoConnection).GetServer();
            MongoDatabase sexStore = mongoServer.GetDatabase(Settings.Default.MongoDatabase);
            Seed seeder = new Seed(sexStore);
            seeder.Initialize();
        }

        /// <summary>
        /// Shows the Sex Store main menu.
        /// </summary>
        private static void ShowMenu()
        {
            Console.WriteLine(MenuSeparator());
            Console.WriteLine("Main Operations:");

            Console.WriteLine("'SP' - Shows all products in the SQLServer Database"); // DONE
            Console.WriteLine("'ZEX - Exports information from ZIP file and loads it to SQLServer'"); // DONE
            Console.WriteLine("'MIP' - Imports products from MongoDB to SQLServer");

            Console.WriteLine();
            Console.WriteLine("PDF Operations:");

            Console.WriteLine("'QPDF' - Generates PDF report for 'Available Quantities'"); // DONE
            Console.WriteLine("'SPDF' - Generates PDF report for 'Sales'"); // DONE

            Console.WriteLine();
            Console.WriteLine("XML Operations:");

            Console.WriteLine("'QXML' - Generates XML report for 'Available Quantities'"); // DONE
            Console.WriteLine("'SXML' - Generates XML report for 'Sales'"); // DONE
            Console.WriteLine("'XMLS' - Imports data from XML to SQLServer");
            Console.WriteLine("'XMLM' - Imports data from XML to MongoDB");

            Console.WriteLine();
            Console.WriteLine("Other Operations:");

            Console.WriteLine("'JSR' - Generates JSON report for 'SALES' and saves the information in MySQL");
            Console.WriteLine("'GEX' - Generates excel report from MySQL and SQLite");

            Console.WriteLine(MenuSeparator());
        }

        /// <summary>
        /// Gets a separator for UI stuff.
        /// </summary>
        /// <param name="separatorLength">Separator length.</param>
        /// <returns>String with given length.</returns>
        private static string MenuSeparator(int separatorLength = 30)
        {
            return new string('-', separatorLength);
        }

        /// <summary>
        /// Executes commands for the Sex Store App.
        /// </summary>
        private static void ExecuteCommands()
        {
            string command = Console.ReadLine().ToLowerInvariant();

            if (command == "sp")
            {
                Console.Clear();
                Console.WriteLine("Showing all products in the SQL Server database...");
                ShowAllProductsInSQLServer();
            }
            else if (command == "zex")
            {
                Console.Clear();
                Console.WriteLine("Exporting information from ZIP file...");

                Console.Write("Enter file path:");
                var filePath = Console.ReadLine();

                if (!filePath.EndsWith("\\"))
                {
                    filePath += "\\";
                }

                Console.Write("Enter file name:");
                var fileName = Console.ReadLine();

                Console.Write("Extraction directory name:");
                var directoryName = Console.ReadLine();

                var zipExtractor = new ZipExtractor(filePath, fileName, directoryName);
                zipExtractor.GetAllReports();

                Console.Clear();
                Console.WriteLine("Data from ZIP imported");
            }
            else if (command == "mip")
            {
                Console.Clear();
                Console.WriteLine("Transfering products data from MongoDB to SQL Server...");
                // TO DO:

                MongoServer mongoServer = new MongoClient(Settings.Default.MongoConnection).GetServer();
                MongoDatabase sexStore = mongoServer.GetDatabase(Settings.Default.MongoDatabase);
                int transferredDocs = 0;

                try
                {
                    TransferEngine transfer = new TransferEngine(sexStore);
                    transferredDocs = transfer.TransferData();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                // IF TRUE
                if (transferredDocs > 0)
                {
                    Console.WriteLine("Data Transferred");
                }
            }
            else if (command == "qpdf")
            {
                Console.Clear();
                Console.WriteLine("Generating PDF report for 'Available Quantities'...");
                PDFExporter.RemainingQuantities();

                Console.Clear();
                Console.WriteLine("Report Generated");
            }
            else if (command == "spdf")
            {
                Console.Clear();
                Console.WriteLine("Generating PDF report for 'Sales'...");
                PDFExporter.AllSales();

                Console.Clear();
                Console.WriteLine("Report Generated");
            }
            else if (command == "qxml")
            {
                Console.Clear();
                Console.WriteLine("Generating XML report for 'Available Quantities'...");
                XMLExporter.RemainingQuantities();

                Console.Clear();
                Console.WriteLine("Report Generated");
            }
            else if (command == "sxml")
            {
                Console.Clear();
                Console.WriteLine("Generating XML report for 'Sales'...");

                XMLExporter.AllSales();

                Console.Clear();
                Console.WriteLine("Report Generated");
            }
            else if (command == "xmls")
            {
                Console.Clear();
                Console.WriteLine("Importing data from XML to SQL Server...");

                Console.Write("Enter file path: ");
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
                Console.Write("Enter file path: ");
                var filePath = Console.ReadLine();
                // TO DO:

                Console.Clear();

                MongoServer mongoServer = new MongoClient(Settings.Default.MongoConnection).GetServer();
                MongoDatabase sexStore = mongoServer.GetDatabase(Settings.Default.MongoDatabase);


                bool isImportSuccessful = false;

                try
                {
                    MongoProductImporter xmlImport = new MongoProductImporter(ImportType.XML, filePath, sexStore);
                    isImportSuccessful = xmlImport.InitializeImport();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                // IF TRUE
                if (isImportSuccessful)
                {
                    Console.WriteLine("Import Completed");
                }
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

        private static void ShowAllProductsInSQLServer()
        {
            var sqlServerConnection = new SQLServerContextFactory().Create();

            using (sqlServerConnection)
            {
                var products = sqlServerConnection.Products;

                Console.WriteLine("##################################################");

                foreach (var product in products)
                {
                    Console.Write("#");
                    Console.WriteLine("{1} - {0} - {3} - {2}", product.Name, product.ProductCode, product.Price, product.Description);
                }

                Console.WriteLine("##################################################");
            }
        }
    }
}
