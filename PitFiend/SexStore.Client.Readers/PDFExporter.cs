namespace SexStore.Client.Readers
{
    using System;
    using System.Linq;
    using iTextSharp.text;
    using System.Text;
    using System.IO;
    using SQLServer.Data;
    using SexStore.Client.Readers.Helpers;
    using System.Reflection;

    public class PDFExporter
    {
        public const string fileType = "pdf";
        public const string pageStart =
            "<!DOCTYPE html>" +
            "<html lang=\"en\"xmlns=\"http://www.w3.org/1999/xhtml \">" +
            "<head>" +
                "<meta charset=\"utf-8\" />" +
                "<title>Report</title>" +
            "</head>" +
            "<body>";

        public const string pageEnd = "</body></html>";

        public static void RemainingQuantities()
        {
            var db = new SQLServerContextFactory().Create();
            var strBuilder = new StringBuilder();
            var products = db.Products.OrderBy(p => p.QuantityInStock);

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

            string methodName = MethodBase.GetCurrentMethod().Name;
            File.WriteAllBytes(Helpers.NamingFactory.BuildName(methodName, fileType), file);
        }

        public static void AllSales()
        {
            var db = new SQLServerContextFactory().Create();
            var strBuilder = new StringBuilder();
            var sales = db.Sales.OrderBy(s => s.Shop.Name);

            strBuilder.Append("<table border='1'>");
            strBuilder.Append("<tr>");
            strBuilder.Append("<th style=\"font-size:16px; text-align:center;\" colspan='5'>Aggregated Sales</th>");
            strBuilder.Append("</tr>");
            strBuilder.Append("<tr>");
            strBuilder.Append("<td>Location</td>");
            strBuilder.Append("<td>Product</td>");
            strBuilder.Append("<td>Quantity</td>");
            strBuilder.Append("<td>Price</td>");
            strBuilder.Append("<td>Sum</td>");
            strBuilder.Append("</tr>");

            foreach (var sale in sales)
            {
                strBuilder.Append("<tr>");
                strBuilder.AppendFormat("<td>{0}</td>", sale.Shop.Name);
                strBuilder.AppendFormat("<td>{0}</td>", sale.Product.Name);
                strBuilder.AppendFormat("<td>{0}</td>", sale.Quantity);
                strBuilder.AppendFormat("<td>{0}</td>", sale.Product.Price);
                strBuilder.AppendFormat("<td>{0}</td>", sale.Quantity * sale.Product.Price);
                strBuilder.Append("</tr>");

            }


            strBuilder.Append("</table>");
            var resultingTable = strBuilder.ToString();

            PDFBuilder.HtmlToPdfBuilder builder = new PDFBuilder.HtmlToPdfBuilder(PageSize.LETTER);
            PDFBuilder.HtmlPdfPage page = builder.AddPage();
            page.AppendHtml(resultingTable);

            byte[] file = builder.RenderPdf();

            string methodName = MethodBase.GetCurrentMethod().Name;
            File.WriteAllBytes(Helpers.NamingFactory.BuildName(methodName, fileType), file);
        }
    }
}
