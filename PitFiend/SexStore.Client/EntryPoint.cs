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
    using System.Xml.Linq;

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
            Console.WriteLine("'MIP' - Imports products from MongoDB to SQLServer"); // DONE

            Console.WriteLine();
            Console.WriteLine("PDF Operations:");

            Console.WriteLine("'QPDF' - Generates PDF report for 'Available Quantities'"); // DONE
            Console.WriteLine("'SPDF' - Generates PDF report for 'Sales'"); // DONE

            Console.WriteLine();
            Console.WriteLine("XML Operations:");

            Console.WriteLine("'QXML' - Generates XML report for 'Available Quantities'"); // DONE
            Console.WriteLine("'SXML' - Generates XML report for 'Sales'"); // DONE
            Console.WriteLine("'XMLS' - Imports data from XML to SQLServer"); // DONE
            Console.WriteLine("'XMLM' - Imports data from XML to MongoDB"); // DONE

            Console.WriteLine();
            Console.WriteLine("Other Operations:");

            Console.WriteLine("'JSR' - Generates JSON report for 'SALES' and saves the information in MySQL"); // DONE
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
 
                MongoToSQL();
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

                ImportXMLData(filePath);

                //Console.Clear();

                // IF TRUE
                Console.WriteLine("Import Completed");
            }
            else if (command == "xmlm")
            {
                Console.Clear();
                Console.WriteLine("Importing data from XML to MongoDB...");
                Console.Write("Enter file path: ");
                var filePath = Console.ReadLine();

                Console.Clear();

                XMLtoMongo(filePath);
            }
            else if (command == "jsr")
            {
                Console.Clear();
                Console.WriteLine("Generating JSON report for 'SALES'");

                JsonReporter.ExportReportToJsonFiles();

                Console.Clear();
                Console.WriteLine("Saving JSON reports in MySQL...");

                MySQLReporter.ExportReportToMySQLDb();

                Console.Clear();
                Console.WriteLine("Operation Completed");
            }
            else if (command == "gex")
            {
                Console.Clear();
                Console.WriteLine("Generating Excel report...");

                Excel2007Reporter.ExportReportToXlsxFile();

                Console.Clear();
                Console.WriteLine("Report Generated");
            }
            else
            {
                Console.Clear();
            }

            Console.WriteLine();
            ShowMenu();
        }

        private static void ImportXMLData(string filePath)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(filePath);

                var sales =
                from sale in xmlDoc.Descendants("sale")
                select new
                {
                    pid = int.Parse(sale.Element("productId").Value),
                    sid = int.Parse(sale.Element("shopId").Value),
                    qua = int.Parse(sale.Element("quantity").Value),
                    date = DateTime.Parse(sale.Element("saleDate").Value),
                };

                var sqlServerConnection = new SQLServerContextFactory().Create();

                foreach (var sale in sales)
                {
                    var newSale = new Sale();
                    newSale.Product = sqlServerConnection.Products.Find(sale.pid);
                    newSale.Shop = sqlServerConnection.Shops.Find(sale.sid);
                    newSale.Quantity = sale.qua;
                    newSale.SaleDate = sale.date;
                    sqlServerConnection.Sales.Add(newSale);
                }

                sqlServerConnection.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return;
            }
        }
 
        private static void XMLtoMongo(string filePath)
        {
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
 
        private static void MongoToSQL()
        {
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
