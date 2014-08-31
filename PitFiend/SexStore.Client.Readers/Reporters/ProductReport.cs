using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLServer.Data;

namespace SexStore.Client.Readers.Reporters
{
    public class ProductReport
    {
        private List<string> shopNames;
        public ProductReport()
        {
            shopNames = new List<string>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> ShopNames 
        {
            get { return this.shopNames; }
        }

        public int TotalQuantitySold { get; set; }

        public double TotalIncomes { get; set; }

        public static void ExportToJson(SQLServerContext db)
        {
            var productsCount = db.Products.Count();
            var productsAndTheirSales = db.Products.Join(
                    db.Sales,
                    p => p.ID,
                    s => s.Product.ID,
                    (p, s) => new
                    {
                        Id = p.ID,
                        Name = p.Name,
                        TotalQuantitySold = s.Quantity,
                        TotalIncomes = s.Quantity * p.Price,
                        ShopName = s.Shop.Name
                    }).ToList();

            var reports = new List<ProductReport>(productsCount);

            for (int i = 1; i <= productsCount; i++)
            {
                var currentProduct = new ProductReport() { Id = i, Name = null, TotalQuantitySold = 0, TotalIncomes = 0};
                foreach (var product in productsAndTheirSales)
                {
                    if (product.Id == currentProduct.Id)
                    {
                        currentProduct.Name = product.Name;
                        currentProduct.ShopNames.Add(product.ShopName);
                        currentProduct.TotalQuantitySold += product.TotalQuantitySold;
                        currentProduct.TotalIncomes += product.TotalIncomes;
                    }
                }

                reports.Add(currentProduct);
            }

            
            foreach (var rep in reports)
            {                
                Console.WriteLine(rep.Id);
                Console.WriteLine(rep.Name);
                Console.WriteLine(rep.TotalQuantitySold);
                Console.WriteLine(rep.TotalIncomes);
                foreach (var shop in rep.ShopNames)
                {
                    Console.WriteLine(shop);
                }                
                Console.WriteLine();
            }
        }

    }
}
