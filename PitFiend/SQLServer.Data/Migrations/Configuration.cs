namespace SQLServer.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    
    using SexStore.Models;

    public sealed class Configuration : DbMigrationsConfiguration<SQLServerContext>
    {
        //// Enable-Migrations -EnableAutomaticMigrations
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SQLServerContext context)
        {
            //// This method will be called after migrating to the latest version.

            // Cities
            var sofiaCity = new City() { ID = 1, Name = "Sofia" };
            var plovdivCity = new City() { ID = 2, Name = "Plovdiv" };
            var varnaCity = new City() { ID = 3, Name = "Varna" };
            var bourgasCity = new City() { ID = 4, Name = "Bourgas" };

            // Categories
            var maleProductsCategory = new Category() { ID = 1, Name = "Male Products" };
            var femaleProductsCategory = new Category() { ID = 2, Name = "Female Products" };
            var unisexCategory = new Category() { ID = 3, Name = "Unisex" };

            // Types
            var vibratorType = new ProductType() { ID = 1, Name = "Vibrator" };
            var sexDollType = new ProductType() { ID = 2, Name = "Sex Doll" };
            var lubricantType = new ProductType() { ID = 3, Name = "Lubricant" };
            var pornMovieType = new ProductType() { ID = 4, Name = "Porn Movies" };

            // Products
            var productOne = new Product()
            {
                ID = 1,
                ProductCode = 1001,
                Name = "Miss Dulboko Gurlo",
                Description = "Sex Doll",
                Price = 99.99,
                Type = sexDollType,
                QuantityInStock = 5,
                Categories = new List<Category>()
                {
                    maleProductsCategory,
                },
            };

            var productTwo = new Product()
            {
                ID = 2,
                ProductCode = 1002,
                Name = "Jumbo Penetrator 3000",
                Description = "WOW",
                Price = 19.99,
                Type = vibratorType,
                QuantityInStock = 99,
                Categories = new List<Category>()
                {
                    femaleProductsCategory,
                },
            };

            var productThree = new Product()
            {
                ID = 3,
                ProductCode = 1003,
                Name = "Dildo Bagings",
                Description = "Small vibrator.",
                Price = 23.99,
                Type = vibratorType,
                QuantityInStock = 11,
                Categories = new List<Category>()
                {
                    femaleProductsCategory,
                },
            };

            var productFour = new Product()
            {
                ID = 4,
                ProductCode = 1004,
                Name = "Exotic Liquids",
                Description = "Lubricant",
                Price = 5.99,
                Type = lubricantType,
                QuantityInStock = 11,
                Categories = new List<Category>()
                {
                    unisexCategory,
                },
            };

            var productFive = new Product()
            {
                ID = 5,
                ProductCode = 1005,
                Name = "How I Banged Your Mother",
                Description = "Porn Movie",
                Price = 11.99,
                Type = pornMovieType,
                QuantityInStock = 4,
                Categories = new List<Category>()
                {
                    unisexCategory,
                },
            };

            var productSix = new Product()
            {
                ID = 6,
                ProductCode = 1006,
                Name = "American Cream Pie",
                Description = "Porn Movie",
                Price = 11.99,
                Type = pornMovieType,
                QuantityInStock = 2,
                Categories = new List<Category>()
                {
                    unisexCategory,
                },
            };

            var productSeven = new Product()
            {
                ID = 7,
                ProductCode = 1007,
                Name = "Whore of the Rings",
                Description = "Porn Movie",
                Price = 11.99,
                Type = pornMovieType,
                QuantityInStock = 3,
                Categories = new List<Category>()
                {
                    unisexCategory,
                },
            };

            // Shops
            var shopOne = new Shop()
            {
                ID = 1,
                Name = "Store #1",
                Address = "Mladost 1",
                City = sofiaCity,
                Products = new List<Product>()
                {
                    productOne,
                    productTwo,
                    productThree,
                    productFour,
                    productFive
                },
            };

            var shopTwo = new Shop()
            {
                ID = 2,
                Name = "Store #2",
                Address = "Drujba 5",
                City = varnaCity,
                Products = new List<Product>()
                {
                    productOne,
                    productTwo,
                    productThree,
                    productFour,
                    productFive,
                    productSix,
                    productSeven
                },
            };

            var shopThree = new Shop()
            {
                ID = 3,
                Name = "Store #3",
                Address = "Liulin 10",
                City = plovdivCity,
                Products = new List<Product>()
                {
                    productOne,
                    productTwo,
                    productThree,
                    productFour,
                    productFive,
                    productSix,
                    productSeven
                },
            };

            var shopFour = new Shop()
            {
                ID = 4,
                Name = "Store #4",
                Address = "Meden Rudnik",
                City = bourgasCity,
                Products = new List<Product>()
                {
                    productOne,
                    productTwo,
                    productThree,
                    productFour,
                    productSix,
                    productSeven
                },
            };

            // Sales
            var saleOne = new Sale()
            {
                ID = 1,
                Product = productOne,
                Shop = shopThree,
                SaleDate = DateTime.Now,
                Quantity = 3
            };

            var saleTwo = new Sale()
            {
                ID = 2,
                Product = productFour,
                Shop = shopTwo,
                SaleDate = DateTime.Now,
                Quantity = 2
            };

            var saleThree = new Sale()
            {
                ID = 3,
                Product = productTwo,
                Shop = shopOne,
                SaleDate = DateTime.Now,
                Quantity = 1
            };

            var saleFour = new Sale()
            {
                ID = 4,
                Product = productThree,
                Shop = shopFour,
                SaleDate = DateTime.Now,
                Quantity = 1
            };

            var saleFive = new Sale()
            {
                ID = 5,
                Product = productSix,
                Shop = shopFour,
                SaleDate = DateTime.Now,
                Quantity = 1
            };

            context.Sales.AddOrUpdate(saleOne);
            context.Sales.AddOrUpdate(saleTwo);
            context.Sales.AddOrUpdate(saleThree);
            context.Sales.AddOrUpdate(saleFour);
            context.Sales.AddOrUpdate(saleFive);
        }
    }
}
