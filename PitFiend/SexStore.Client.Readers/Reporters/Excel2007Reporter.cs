namespace SexStore.Client.Readers.Reporters
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using MySQLServer;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using SQLiteServer.Data;
    using SexStore.Client.Readers.Helpers;

    public static class Excel2007Reporter
    {
        public static void ExportReportToXlsxFile()
        {
            //var mySQLdb = new MySQLContext("MySQLConnStrDKostovLaptop");
            var sqLitedb = new SQLiteServConnection(@"Data Source=..\..\..\SQLiteServer.Data\SexStoreProductInfo.sqlite;Version=3;");

            var reportsFromMySQL = ProductReportsCreator.CreateReportForEveryProductFromMySQL();
            reportsFromMySQL.OrderBy(x => x.ProductCode);

            var reportsFromSQLite = sqLitedb.GetProductsInformation();

            var newFile = new FileInfo(@"..\..\..\Reports\ExcelReports\Profits report.xlsx");
            if (newFile.Exists)
            {
                newFile.Delete();
            }

            ExcelPackage xlPackage = new ExcelPackage(newFile);

            using (xlPackage)
            {
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("Profit report");

                worksheet.Cells[1, 1].Value = "Product code";
                worksheet.Cells[1, 2].Value = "Product name";
                worksheet.Cells[1, 3].Value = "Sold in";
                worksheet.Cells[1, 4].Value = "Incomes";
                worksheet.Cells[1, 5].Value = "Taxes";
                worksheet.Cells[1, 6].Value = "Expenses";
                worksheet.Cells[1, 7].Value = "Total profit";

                var columnsCount = worksheet.Dimension.End.Column;

                var row = 2;
                foreach (var product in reportsFromMySQL)
                {
                    var productTaxAndExpenses = reportsFromSQLite
                        .Where(r => r.ProductCode.Equals(product.ProductCode))
                        .Select(r => 
                            new 
                            {
                                tax = r.TaxPercent, 
                                expenses = r.Expenses
                            })
                        .FirstOrDefault();
                    
                    double profit;
                    int tax;
                    double expenses;
                    if (productTaxAndExpenses != null)
                    {
                        tax = productTaxAndExpenses.tax;
                        expenses = productTaxAndExpenses.expenses;
                        profit = (product.TotalIncomes - (product.TotalIncomes * ((double)tax / 100d))) - expenses;
                    }
                    else
                    {
                        tax = 0;
                        expenses = 0;
                        profit = product.TotalIncomes;
                    }

                    worksheet.Cells[row, 1].Value = product.ProductCode;
                    worksheet.Cells[row, 2].Value = product.Name;
                    worksheet.Cells[row, 3].Value = (product.ShopNames.Count > 0 ? product.ShopNames[0] : "");
                    worksheet.Cells[row, 4].Value = product.TotalIncomes;
                    worksheet.Cells[row, 5].Value = tax;
                    worksheet.Cells[row, 6].Value = expenses;
                    worksheet.Cells[row, 7].Value = profit;

                    row++;
                }

                for (int i = 1; i <= columnsCount; i++)
                {
                    worksheet.Cells[1, i].Style.Font.Size = 12;
                    worksheet.Cells[1, i].Style.Font.Bold = true;
                    worksheet.Column(i).Style.Border.BorderAround(ExcelBorderStyle.Medium);
                    worksheet.Column(i).AutoFit();
                }

                xlPackage.Save();
            }
        }
    }
}