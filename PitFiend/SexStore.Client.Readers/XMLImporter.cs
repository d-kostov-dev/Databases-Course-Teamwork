namespace SexStore.Client.Readers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

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
