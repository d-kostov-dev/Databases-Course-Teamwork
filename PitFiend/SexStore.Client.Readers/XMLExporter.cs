namespace SexStore.Client.Readers
{
    using System;
    using System.Linq;
    using SQLServer.Data;
    using System.Xml.Linq;

    /// <summary>
    /// Exports the data from the database into an XML file
    /// </summary>
    public class XMLExporter
    {
        /// <summary>
        /// Exports to XML all remaining products and the shops they are located in
        /// </summary>
        /// <param name="db"></param>
        public static void ExportRemainingQuantitiesToXml(SQLServerContext db)
        {
            //create root element
            XElement root = new XElement("products");

            //make a collection with all the data you want to export to XML. Use as many joins as needed
            var products = db.Products;

            //go through all items in the collection
            foreach (var product in products)
            {
                //for every nested element you must create new instance of XElement
                XElement currentProduct = new XElement("product"); //create tag
                currentProduct.SetAttributeValue("name", product.Name); //set attribute
                currentProduct.SetAttributeValue("description", product.Description); //set another attribute
         
                XElement productInfo = new XElement("info"); //nest element after "Product"
                productInfo.Add(new XElement("price", product.Price)); //add element inside "Info" 
                //you can create those as new XElement like the creation of "Info"
                productInfo.Add(new XElement("quantity", product.QuantityInStock));
         
                //add info to product
                currentProduct.Add(productInfo);
         
                //add current set of tags to root
                root.Add(currentProduct);
            }

            //imagine this like the javascript appendChild stuff when adding elements in the DOM





            string tempFileName = string.Format("{0}-{1}.xml", "Remaining quantities", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
            root.Save("../../../Reports/XMLReports/" + tempFileName);
            Console.WriteLine("Example quantities exported to XML");
        }


        /// <summary>
        /// TODO - Export to XML all sales that happened in the last month
        /// </summary>
        public static void ExportSalesForLastMonth(SQLServerContext database)
        {

        }
    }
}
