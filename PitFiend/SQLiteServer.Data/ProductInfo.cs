using System;
namespace SQLiteServer.Data
{
    public class ProductInfo
    {
        public ProductInfo(int code, string productName, int productTax)
        {
            this.ProductCode = code;
            this.ProductName = productName;
            this.TaxPercent = productTax;
        }

        public int ProductCode { get; set; }

        public string ProductName { get; set; }

        public int TaxPercent { get; set; }
    }
}
