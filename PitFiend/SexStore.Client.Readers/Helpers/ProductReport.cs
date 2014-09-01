namespace SexStore.Client.Readers.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductReport
    {
        private List<string> shopNames;

        public ProductReport()
        {
            this.shopNames = new List<string>();
        }

        public int Id { get; set; }

        public int ProductCode { get; set; }

        public string Name { get; set; }

        public List<string> ShopNames 
        {
            get { return this.shopNames; }
        }

        public int TotalQuantitySold { get; set; }

        public double TotalIncomes { get; set; }
    }
}
