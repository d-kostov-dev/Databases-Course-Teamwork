namespace SexStore.Client.Readers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
        
    using MySQLServer;
    using SQLiteServer.Data;
    

    public class XLSXExporter
    {
        public static void ExportXlsxReport(SQLiteServConnection sqLiteConn)//, MySQLContext mySqlConn)
        {
            //var sqLiteConn =
            //        new SQLiteServConnection(@"Data Source=..\..\..\SQLiteServer.Data\SexStoreProductInfo.sqlite;Version=3;");
            //var productsInfo = sqLiteConn.GetProductsInformation();

            //foreach (var product in productsInfo)
            //{
            //    Console.WriteLine("Product Code: {0}, Name: {1}, Tax: {2}", product.ProductCode, product.ProductName, product.TaxPercent);
            //}

            var mySQLConnection = new MySQLContext("MySQLConnStrGYaramov");

            using (mySQLConnection)
            {
                var allReports = mySQLConnection.sexStoreReports;
                foreach (var report in allReports)
                {
                    Console.WriteLine(report.product_code + " " + report.product_name);
                }
                //var newReport = new sexStoreReports() { Id = 3, product_code = 1001, product_name = "Miss Dulboko Gurlo" };
                //mySQLConnection.Add(newReport);
                //mySQLConnection.SaveChanges();
            }
        }
    }
}
