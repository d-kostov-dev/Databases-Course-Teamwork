namespace SexStore.Client.Readers.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SQLServer.Data;
    using MySQLServer;

    public static class ProductReportsCreator
    {
        public static List<ProductReport> CreateReportForEveryProductFromSQLServer()
        {
            SQLServerContext db = new SQLServerContextFactory().Create();

            var reports = new List<ProductReport>();

            using (db)
            {
                var productsCount = GetProductsCount(db);
                var productSalesData = GetProductsDataFromDb(db);

                reports = new List<ProductReport>(productsCount);

                for (int i = 1; i <= productsCount; i++)
                {
                    var currentProduct = new ProductReport(i);
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

        public static List<ProductReport> CreateReportForEveryProductFromMySQL()
        {
            MySQLContext db = new MySQLContext("MySQLConnStrDKostovLaptop");

            var reports = new List<ProductReport>();

            using (db)
            {
                var productsCount = GetProductsCount(db);
                var productSalesData = GetProductsDataFromDb(db);
                var firstProductId = GetFirstProductId(db);

                reports = new List<ProductReport>(productsCount);

                var length = firstProductId + productsCount;
                for (int i = firstProductId; i < length; i++)
                {
                    var currentProduct = new ProductReport(i);
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
            var productsAndTheirSales = db.Products.GroupJoin(
                    db.Sales,
                    p => p.ID,
                    s => s.Product.ID,
                    (p, s) => new DbDataHelpType()
                    {
                        Id = p.ID,
                        ProductCode = p.ProductCode,
                        Name = p.Name,
                        TotalQuantitySold = s.DefaultIfEmpty().Select(x => x.Quantity).FirstOrDefault(),
                        TotalIncomes = s.DefaultIfEmpty().Select(x => x.Quantity).FirstOrDefault() * p.Price,
                        ShopName = s.DefaultIfEmpty().Select(x => x.Shop.Name).FirstOrDefault()
                    }).ToList();

            return productsAndTheirSales;
        }

        private static List<DbDataHelpType> GetProductsDataFromDb(MySQLContext db)
        {
            var productsAndTheirSales = db.sexStoreReports
                .Select(r => new DbDataHelpType()
                {
                    Id = r.Id,
                    ProductCode = r.ProductCode,
                    Name = r.Name,
                    TotalQuantitySold = r.TotalQuantitySold,
                    TotalIncomes = r.TotalIncomes,
                    ShopName = r.SoldInShops
                }).ToList();

            return productsAndTheirSales;
        }

        private static int GetProductsCount(SQLServerContext db)
        {
            var productsCount = db.Products.Count();

            return productsCount;
        }

        private static int GetProductsCount(MySQLContext db)
        {
            db.sexStoreReports.Count();
            var productsCount = db.sexStoreReports.Count();

            return productsCount;
        }

        private static int GetFirstProductId(MySQLContext db)
        {
            var id = db.sexStoreReports.Select(x => x.Id).FirstOrDefault();
            return id;
        }
    }
}
