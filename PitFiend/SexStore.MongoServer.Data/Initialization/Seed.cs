namespace SexStore.MongoServer.Data.Initialization
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
                Console.WriteLine("Exception: {0}\n{1}", ex.Message, ex.StackTrace);
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
                    new ProductType("Whip"),
                    new ProductType("Shrink Vagina Pills"),
                    new ProductType("Stimulants")
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
                    new City("Pernik"),
                    new City("Qmbol"),
                    new City("Vidin")
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
                    new Product("O-Tight", "Giving her first time experience", 1501, 14.99M, 100, this.ProductTypesSeeds[1].Id, new[] { 2 }),
                    new Product("Bull Performance", "-", 1502, 19.99M, 59, this.ProductTypesSeeds[2].Id, new[] { 1 }),
                    new Product("Leather Whip", "Pervert's must-have tool", 1503, 46.50M, 11, this.ProductTypesSeeds[0].Id, new[] { 3 }),
                    new Product("B(.)(.)Bster Whip", "Special leather whip", 1504, 149.99M, 25, this.ProductTypesSeeds[0].Id, new[] { 2 })
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
                    new Shop("Store #5", "Sveti 'Golf Mk3 GTI' 5", this.CitiesSeeds[0].Id, products),
                    new Shop("Store #6", "Musala 69", this.CitiesSeeds[2].Id, products),
                    new Shop("Store #7", "Drujba 3", this.CitiesSeeds[1].Id, products),
                };

                shopsCollection.InsertBatch<Shop>(shopsBatch);
            }
        }
    }
}
