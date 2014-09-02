namespace SexStore.Client.Readers.Reporters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MySQLServer;
    using SexStore.Client.Readers.Helpers;    

    public static class MySQLReporter
    {
        public static void ExportReportToMySQLDb()
        {
            MySQLContext db = new MySQLContext("MySQLConnStrDKostovLaptop");

            IList<ProductReport> reports = ProductReportsCreator.CreateReportForEveryProductFromSQLServer();

            using (db)
            {
                foreach (var report in reports)
                {
                    var newReport = new sexStoreReports()
                    {
                        //Id = report.Id,
                        ProductCode = report.ProductCode,
                        Name = report.Name,
                        SoldInShops = string.Join(" ", report.ShopNames),
                        TotalQuantitySold = report.TotalQuantitySold,
                        TotalIncomes = report.TotalIncomes
                    };
                    db.Add(newReport);
                }

                db.SaveChanges();
            }
        }
    }
}
