namespace SexStore.MongoServer.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoServer.Models;

    public sealed class Seed
    {
        public Seed(MongoDatabase database)
        {
            this.Database = database;
        }

        public MongoDatabase Database { get; private set; }

        private IList<ProductType> ProductTypesSeeds { get; set; }

        private IList<City> CitiesSeeds { get; set; }

        private IList<Product> ProductsSeeds { get; set; }

        public void Initialize()
        {
            try
            {
                this.SeedProductType();
                this.SeedCities();
                this.SeedProducts();
                this.SeedShops();
            }
            catch (MongoException ex)
            {
                Console.WriteLine("MongoException: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
            }
        }

        private void SeedProductType()
        {
            MongoCollection<BsonDocument> productTypesCollection = this.Database.GetCollection("ProductTypes");
            IndexKeysBuilder indexKey = new IndexKeysBuilder().Ascending("Name");
            productTypesCollection.CreateIndex(indexKey, IndexOptions.SetUnique(true));

            if (productTypesCollection.Count() == 0)
            {
                this.ProductTypesSeeds = new ProductType[]
                {
                    new ProductType("Vibrator"),
                    new ProductType("Sex Doll"),
                    new ProductType("Lubricant")
                };

                productTypesCollection.InsertBatch<ProductType>(this.ProductTypesSeeds);
            }
        }

        private void SeedCities()
        {
            MongoCollection<BsonDocument> citiesCollection = this.Database.GetCollection("Cities");
            IndexKeysBuilder indexKey = new IndexKeysBuilder().Ascending("Name");
            citiesCollection.CreateIndex(indexKey, IndexOptions.SetUnique(true));

            if (citiesCollection.Count() == 0)
            {
                this.CitiesSeeds = new City[]
                {
                    new City("Sofia"),
                    new City("Plovdiv"),
                    new City("Varna"),
                    new City("Bourgas")
                };

                citiesCollection.InsertBatch<City>(this.CitiesSeeds);
            }
        }

        private void SeedProducts()
        {
            MongoCollection<BsonDocument> productsCollection = this.Database.GetCollection("Products");
            IndexKeysBuilder indexKey = new IndexKeysBuilder().Ascending("ProductCode");
            productsCollection.CreateIndex(indexKey, IndexOptions.SetUnique(true));

            if (productsCollection.Count() == 0)
            {
                if (this.ProductTypesSeeds.Count == 0)
                {
                    throw new MongoException("'ProductTypes' seed collection is empty, cannot proceed with filling the 'Products' collection.");
                }

                // Category IDs correspond to MS SQL table 'Categories'
                this.ProductsSeeds = new Product[]
                {
                    new Product("Miss Dulboko Gurlo", "A sex doll", 1001, 99.99M, 5, this.ProductTypesSeeds[1].Id, new[] { 1 }),
                    new Product("Jumbo Penetrator 3000", "WOW", 1002, 19.99M, 99, this.ProductTypesSeeds[0].Id, new[] { 2 }),
                    new Product("Dildo Bagings", "Small vibrator", 1003, 23.99M, 11, this.ProductTypesSeeds[0].Id, new[] { 2 }),
                    new Product("Exotic Liquids", "Slippy lubricant", 1004, 5.99M, 11, this.ProductTypesSeeds[2].Id, new[] { 3 })
                };

                productsCollection.InsertBatch<Product>(this.ProductsSeeds);
            }
        }

        private void SeedShops()
        {
            MongoCollection<BsonDocument> shopsCollection = this.Database.GetCollection("Shops");
            IndexKeysBuilder indexKey = new IndexKeysBuilder().Ascending("Name");
            shopsCollection.CreateIndex(indexKey, IndexOptions.SetUnique(true));

            if (shopsCollection.Count() == 0)
            {
                if (this.CitiesSeeds.Count == 0)
                {
                    throw new MongoException("'Cities' seed collection is empty, cannot proceed with filling the 'Shops' collection.");
                }

                if (this.ProductsSeeds.Count == 0)
                {
                    throw new MongoException("'Products' seed collection is empty, cannot proceed with filling the 'Shops' collection.");
                }

                ICollection<ObjectId> products = new HashSet<ObjectId>();

                foreach (Product product in this.ProductsSeeds)
                {
                    products.Add(product.Id);
                }

                ICollection<Shop> shopsBatch = new HashSet<Shop>()
                {
                    new Shop("Store #1", "Mlasdost 1", this.CitiesSeeds[0].Id, products),
                    new Shop("Store #2", "Drujba 5", this.CitiesSeeds[2].Id, products),
                    new Shop("Store #3", "Liulin 10", this.CitiesSeeds[1].Id, products),
                    new Shop("Store #4", "Meden Rudnik", this.CitiesSeeds[3].Id, products)
                };

                shopsCollection.InsertBatch<Shop>(shopsBatch);
            }
        }
    }
}
