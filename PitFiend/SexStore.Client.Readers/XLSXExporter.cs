namespace SexStore.Client.Readers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using OfficeOpenXml;
    using OfficeOpenXml.Style;

    class Program
    {
        static void Main()
        {
            var newFile = new FileInfo(@"..\Test.xlsx");
            if (newFile.Exists)
            {
                newFile.Delete();
            }

            ExcelPackage xlPackage = new ExcelPackage(newFile);

            using (xlPackage)
            {
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("Profit report");



                // write some titles into column 1
                worksheet.Cells[1, 1].Value = "Product code";
                worksheet.Cells[1, 2].Value = "Product name";
                worksheet.Cells[1, 3].Value = "Sold in";
                worksheet.Cells[1, 4].Value = "Incomes";
                worksheet.Cells[1, 5].Value = "Taxes";
                worksheet.Cells[1, 6].Value = "Total profit";



                var columnsCount = worksheet.Dimension.End.Column;

                for (int i = 1; i <= columnsCount; i++)
                {
                    worksheet.Cells[1, i].Style.Font.Size = 12;
                    worksheet.Cells[1, i].Style.Font.Bold = true;
                    worksheet.Column(i).Style.Border.BorderAround(ExcelBorderStyle.Medium);
                    worksheet.Column(i).AutoFit();
                }



                var tash = 0;
                xlPackage.Save();
            }




        }
    }
    //using System;
    //using System.Data.Entity;
    //using System.Linq;
        
    //using MySQLServer;
    //using SQLiteServer.Data;

    //public class XLSXExporter
    //{
    //    public static void ExportXlsxReport(SQLiteServConnection sqLiteConn)//, MySQLContext mySqlConn)
    //    {

    //    }
    //}
}
