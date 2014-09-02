namespace SexStore.Client.Readers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using HelperStructures;

    public class XMLImporter
    {
        public static List<Tuple<string, string, string>> GetObjects(string fileName)
        {
            List<Tuple<string, string, string>>
            expenses = new List<Tuple<string, string, string>>();

            XDocument xmlDoc = XDocument.Load(fileName);

            foreach (var sale in xmlDoc.Descendants("sale"))
            {
                string vendorName = sale.Attribute("vendor").Value;

                foreach (var expense in sale.Descendants("expenses"))
                {
                    string month = expense.Attribute("month").Value;
                    string value = expense.Value;

                    expenses.Add(new Tuple<string, string, string>(vendorName, month, value));
                }
            }

            return expenses;
        }

        public static List<Product> GetProducts(string fileName)
        {
            List<Product> products = new List<Product>();

            XDocument xmlDoc = XDocument.Load(fileName);

            foreach (var product in xmlDoc.Descendants("product"))
            {
                // Attributes
                string name = product.Attribute("name").Value;
                int productCode = int.Parse(product.Attribute("code").Value);
                string type = product.Attribute("type").Value;

                // Info tag
                var productInfo = product.Element("info");

                // Info's elements
                string description = productInfo.Element("description").Value;
                decimal price = decimal.Parse(productInfo.Element("price").Value);
                int quantity = int.Parse(productInfo.Element("quantity").Value);

                // Category IDs
                List<int> categoryIds = new List<int>();
                foreach (var category in product.Descendants("category-id"))
                {
                    categoryIds.Add(int.Parse(category.Value));
                }

                // Shop names
                List<string> shops = new List<string>();
                foreach (var shop in product.Descendants("shop"))
                {
                    shops.Add(shop.Value);
                }

                Product currentProduct = new Product()
                {
                    Name = name,
                    ProductCode = productCode,
                    Description = description,
                    Type = type,
                    Price = price,
                    Quantity = quantity,
                    CategoryIds = categoryIds,
                    Shops = shops
                };

                products.Add(currentProduct);
            }

            return products;
        }

      //  public static void ImportExpensesReportsFromXml()
      //  {
      //      var expenses = GetObjects("..\\..\\..\\Vendors-Expenses.xml");
      //      foreach (var expense in expenses)
      //      {
      //          newExpense.VendorName = expense.Item1;
      //          newExpense.Month = expense.Item2;
      //          newExpense.Value = expense.Item3;
      //
      //          Database.SaveData(newExpense);
      //      }
      //  }
    }
}
