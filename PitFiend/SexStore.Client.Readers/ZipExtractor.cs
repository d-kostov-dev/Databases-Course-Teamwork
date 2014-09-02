namespace SexStore.Client.Readers
{
    using SQLServer.Data;
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.IO.Compression;
    using SexStore.Models;

    public class ZipExtractor
    {
        private readonly string extactionFoder;
        private readonly string pathToArchive;
        private readonly string archiveName;

        public ZipExtractor(string pathToArchive, string fileName, string extractFolderName)
        {
            this.pathToArchive = pathToArchive;
            this.archiveName = fileName;
            this.extactionFoder = extractFolderName;
        }

        public void GetAllReports()
        {
            this.ExtractFromArchive();
            this.ParseReports();
        }

        private void ExtractFromArchive()
        {
            if (Directory.Exists(pathToArchive + extactionFoder))
            {
                Directory.Delete(pathToArchive + extactionFoder, true);
            }

            ZipFile.ExtractToDirectory(this.pathToArchive + this.archiveName, this.pathToArchive + extactionFoder);
        }

        private void ParseReports()
        {
            var allFolders = Directory.GetDirectories(this.pathToArchive + extactionFoder);

            foreach (var folder in allFolders)
            {
                var folderName = Path.GetFileName(folder);
                var allFiles = Directory.GetFiles(folder);

                foreach (var file in allFiles)
                {
                    ParseExcelFile(folderName, file);
                }
            }
        }

        private void ParseExcelFile(string folderName, string pathOfFile)
        {
            var connectionString = 
                string.Format(
                "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0;",
                pathOfFile);

            OleDbConnection excelConnection = new OleDbConnection(connectionString);

            excelConnection.Open();

            using (excelConnection)
            {
                var dataTable = new DataTable();
                var adapter = new OleDbDataAdapter("SELECT * FROM [Sales$] ", excelConnection);

                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    var shopID = int.Parse(row.ItemArray[0].ToString());
                    var productID = int.Parse(row.ItemArray[1].ToString());
                    var quantity = int.Parse(row.ItemArray[2].ToString());
                    var date = DateTime.Parse(folderName);
                    InsertToDatabase(shopID, productID, quantity, date);
                }
            }
        }

        private void InsertToDatabase(int shopID, int productID, int quantity, DateTime date)
        {
            var dbConnection = new SQLServerContextFactory().Create();

            using (dbConnection)
            {
                var sale = new Sale();
                sale.Shop = dbConnection.Shops.Find(shopID);
                sale.Product = dbConnection.Products.Find(productID);
                sale.Quantity = quantity;
                sale.SaleDate = date;

                dbConnection.Sales.Add(sale);
                dbConnection.SaveChanges();
            }
        }
    }
}
