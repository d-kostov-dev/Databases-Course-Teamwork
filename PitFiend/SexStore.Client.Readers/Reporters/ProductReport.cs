using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SexStore.Client.Readers.Reporters
{
    public class ProductReport
    {
        public ProductReport()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string ShopName { get; set; }

        public int TotalQuantitySold { get; set; }

        public decimal TotalIncomes { get; set; }

    }
}
