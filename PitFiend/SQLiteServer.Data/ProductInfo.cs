using System;
namespace SQLiteServer.Data
{
    public class ProductInfo
    {
        public ProductInfo(string productName, int productTax)
        {
            this.ProductName = productName;
            this.TaxPercent = productTax;
        }

        public string ProductName { get; set; }

        public int TaxPercent { get; set; }
    }
}
