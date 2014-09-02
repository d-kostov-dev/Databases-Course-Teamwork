namespace SexStore.Client.Readers
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.IO.Compression;

    public class ZipExtractor
    {
        private const string ExtactionFoder = "Reports";

        private readonly string pathToArchive;
        private readonly string archiveName;

        public ZipExtractor(string pathToArchive, string fileName)
        {
            this.pathToArchive = pathToArchive;
            this.archiveName = fileName;
        }

        public void ExtractFromArchive()
        {
            if (Directory.Exists(pathToArchive + ExtactionFoder))
            {
                Directory.Delete(pathToArchive + ExtactionFoder, true);
            }

            ZipFile.ExtractToDirectory(this.pathToArchive + this.archiveName, this.pathToArchive + ExtactionFoder);

            var allFolders = Directory.GetDirectories(this.pathToArchive + ExtactionFoder);
            foreach (var folder in allFolders)
            {
                var folderName = Path.GetFileName(folder);
                var allFiles = Directory.GetFiles(folder);

                foreach (var file in allFiles)
                {
                    ExcelParser(folderName, file);
                }
            }
        }

        private void ExcelParser(string folderName, string pathOfFile)
        {
            var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0;", pathOfFile);
            OleDbConnection connection = new OleDbConnection(connectionString);

            connection.Open();
            using (connection)
            {
                var dataTable = new DataTable();
                var adapter = new OleDbDataAdapter("select * from [Tickets$] ", connection);

                adapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    var companyID = int.Parse(row.ItemArray[0].ToString());
                    var customerID = int.Parse(row.ItemArray[1].ToString());
                    var destinationID = int.Parse(row.ItemArray[2].ToString());
                    var price = decimal.Parse(row.ItemArray[3].ToString());
                    var date = DateTime.Parse(folderName);
                    InsertToDatabase(companyID, customerID, destinationID, price, date);
                }
            }
        }

        private void InsertToDatabase(int companyId, int customerId, int destinationId, decimal price, DateTime date)
        {
            using (var db = new AirportDbContext())
            {
                var ticket = new Ticket();
                ticket.Company = db.Companies.Find(companyId);
                ticket.Customer = db.Customers.Find(customerId);
                ticket.Destination = db.Destinations.Find(destinationId);
                ticket.TravelingDate = date;
                ticket.Price = price;


                db.Tickets.Add(ticket);
                db.SaveChanges();
            }
        }
    }
}
