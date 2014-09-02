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
            this.TotalIncomes = 0;
            this.TotalQuantitySold = 0;
        }

        public ProductReport(int id)
            : this()
        {
            this.Id = id;
            this.TotalIncomes = 0;
            this.TotalQuantitySold = 0;
        }

        public ProductReport(int id, string name)
            : this(id)
        {
            this.Name = name;
            this.TotalIncomes = 0;
            this.TotalQuantitySold = 0;
        }

        public ProductReport(int id, string name, int totalQuatitySold)
            : this(id, name)
        {

            this.TotalIncomes = 0;
            this.TotalQuantitySold = totalQuatitySold;
        }

        public ProductReport(int id, string name, int totalQuatitySold, int totalIncomes)
            : this(id, name, totalQuatitySold)
        {
            this.TotalIncomes = totalIncomes;
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
