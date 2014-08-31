namespace SexStore.Client.Readers.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SQLServer.Data;

    public static class ProductReportsCreator
    {
        public static List<ProductReport> CreateReportForEveryProduct(SQLServerContext db)
        {
            var reports = new List<ProductReport>();

            using (db)
            {
                var productsCount = GetProductsCount(db);
                var productSalesData = GetProductsDataFromDb(db);

                reports = new List<ProductReport>(productsCount);

                for (int i = 1; i <= productsCount; i++)
                {
                    var currentProduct = new ProductReport() { Id = i, Name = null, TotalQuantitySold = 0, TotalIncomes = 0 };
                    foreach (var product in productSalesData)
                    {
                        if (product.Id == currentProduct.Id)
                        {
                            currentProduct.ProductCode = product.ProductCode;
                            currentProduct.Name = product.Name;
                            currentProduct.ShopNames.Add(product.ShopName);
                            currentProduct.TotalQuantitySold += product.TotalQuantitySold;
                            currentProduct.TotalIncomes += product.TotalIncomes;
                        }
                    }

                    reports.Add(currentProduct);
                }
            }            

            return reports;
        }

        private static List<DbDataHelpType> GetProductsDataFromDb(SQLServerContext db)
        {
            var productsAndTheirSales = db.Products.Join(
                    db.Sales,
                    p => p.ID,
                    s => s.Product.ID,
                    (p, s) => new DbDataHelpType()
                    {
                        Id = p.ID,
                        ProductCode = p.ProductCode,
                        Name = p.Name,
                        TotalQuantitySold = s.Quantity,
                        TotalIncomes = s.Quantity * p.Price,
                        ShopName = s.Shop.Name
                    }).ToList();

            return productsAndTheirSales;
        }

        private static int GetProductsCount(SQLServerContext db)
        {
            var productsCount = db.Products.Count();

            return productsCount;
        }
    }
}
