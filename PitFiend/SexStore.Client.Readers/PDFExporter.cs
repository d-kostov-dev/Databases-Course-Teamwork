namespace SexStore.Client.Readers
{
    using System;
    using System.Linq;
    using iTextSharp.text;
    using System.Text;
    using System.IO;
    using SQLServer.Data;
    using SexStore.Client.Readers.Helpers;

    public class PDFExporter
    {
        public static void ExportRemainingQuantitiesToPdf(SQLServerContext db)
        {
            const string pageStart =
                "<!DOCTYPE html>" +
                "<html lang=\"en\"xmlns=\"http://www.w3.org/1999/xhtml \">" +
                "<head>" +
                    "<meta charset=\"utf-8\" />" +
                    "<title>Report</title>" +
                "</head>" +
                "<body>";

            const string pageEnd = "</body></html>";

            //get the database and make stringbuilder to append elements
            var strBuilder = new StringBuilder();
            //make a collection with all the data you want to export to XML. Use as many joins as needed
            var products = db.Products;

            strBuilder.Append("<table border='1'>");
            strBuilder.Append("<tr>");
            strBuilder.Append("<th style=\"font-size:16px; text-align:center;\" colspan='4'>Available products</th>");
            strBuilder.Append("</tr>");
            strBuilder.Append("<tr>");
            strBuilder.Append("<td>Product Name</td>");
            strBuilder.Append("<td>Description</td>");
            strBuilder.Append("<td>Quantity in stock</td>");
            strBuilder.Append("<td>Price</td>");
            strBuilder.Append("</tr>");

            foreach (var product in products)
            {
                strBuilder.Append("<tr>");
                strBuilder.AppendFormat("<td>{0}</td>", product.Name);
                strBuilder.AppendFormat("<td>{0}</td>", product.Description);
                strBuilder.AppendFormat("<td>{0}</td>", product.QuantityInStock);
                strBuilder.AppendFormat("<td>{0}</td>", product.Price);
                strBuilder.Append("</tr>");
            }

            strBuilder.Append("</table>");

            PDFBuilder.HtmlToPdfBuilder builder = new PDFBuilder.HtmlToPdfBuilder(PageSize.LETTER);
            PDFBuilder.HtmlPdfPage page = builder.AddPage();
            page.AppendHtml(strBuilder.ToString());

            byte[] file = builder.RenderPdf();
            string tempFolder = "../../../Reports/PDFReports/";
            string tempFileName = string.Format("{0}-{1}.pdf", "Remaining quantities", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
            File.WriteAllBytes(tempFolder + tempFileName, file);
            Console.WriteLine("PDF successfully generated");
        }
    }
}
