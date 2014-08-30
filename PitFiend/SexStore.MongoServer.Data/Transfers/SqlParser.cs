namespace SexStore.MongoServer.Data.Transfers
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using Mongo = MongoServer.Models;
    using SQL = SexStore.Models;

    // Under development
    public sealed class SqlParser
    {
        public SqlParser(MongoDatabase database)
        {
            this.Database = database;

            ////this.InitializeParsing();
        }

        public MongoDatabase Database { get; set; }

        private IDictionary<ObjectId, SQL.City> ParsedCities { get; set; }

        private IDictionary<ObjectId, SQL.ProductType> ParsedTypes { get; set; }

        private IDictionary<ObjectId, SQL.Product> ParsedProducts { get; set; }

        private ISet<SQL.Shop> ParsedShops { get; set; }

        private void InitializeParsing()
        {
            try
            {
                this.ParseProductTypesToSql();
                this.ParseCitiesToSql();
                this.ParseProductsToSql();
                this.ParseShopsToSql();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {1}", ex.Message);
            }
        }

        private void ParseProductTypesToSql()
        {
            var productTypes = this.GetCursor<Mongo.ProductType>("ProductTypes");
            this.ParsedTypes = new Dictionary<ObjectId, SQL.ProductType>();

            foreach (Mongo.ProductType type in productTypes)
            {
                SQL.ProductType current = new SQL.ProductType()
                {
                    Name = type.Name
                };

                this.ParsedTypes.Add(type.Id, current);
            }
        }

        private void ParseCitiesToSql()
        {
            var cities = this.GetCursor<Mongo.City>("Cities");
            this.ParsedCities = new Dictionary<ObjectId, SQL.City>();

            foreach (Mongo.City city in cities)
            {
                SQL.City current = new SQL.City()
                {
                    Name = city.Name
                };

                this.ParsedCities.Add(city.Id, current);
            }
        }

        private void ParseProductsToSql()
        {
            var products = this.GetCursor<Mongo.Product>("Products");
            this.ParsedProducts = new Dictionary<ObjectId, SQL.Product>();

            foreach (Mongo.Product product in products)
            {
                ObjectId currentTypeId = product.TypeId;
                SQL.ProductType currentProductType;

                if (!this.ParsedTypes.TryGetValue(currentTypeId, out currentProductType))
                {
                    //// throw exception
                }

                SQL.Product current = new SQL.Product()
                {
                    ProductCode = product.ProductCode,
                    Name = product.Name,
                    Description = product.Description,
                    Price = (double)product.Price,
                    Type = currentProductType,
                    QuantityInStock = product.UnitsInStock,
                    Categories = new List<SQL.Category>()
                    {
                        //// Requires MSSQL fetch query
                    }
                };

                this.ParsedProducts.Add(product.Id, current);
            }
        }

        private void ParseShopsToSql()
        {
            var shops = this.GetCursor<Mongo.Shop>("Shops");
            this.ParsedShops = new HashSet<SQL.Shop>();

            foreach (Mongo.Shop shop in shops)
            {
                ObjectId currentCityId = shop.CityId;
                SQL.City currentCity;
                
                if (!this.ParsedCities.TryGetValue(currentCityId, out currentCity))
                {
                    //// throw exception
                }

                HashSet<SQL.Product> currentProducts = new HashSet<SQL.Product>();

                foreach (ObjectId productId in shop.ProductIds)
                {
                    SQL.Product currentProduct;
                    
                    if (!this.ParsedProducts.TryGetValue(productId, out currentProduct))
                    {
                        //// throw exception
                    }

                    currentProducts.Add(currentProduct);
                }

                SQL.Shop current = new SQL.Shop()
                {
                    Name = shop.Name,
                    Address = shop.Address,
                    City = currentCity,
                    Products = currentProducts
                };

                this.ParsedShops.Add(current);
            }
        }

        private MongoCursor<T> GetCursor<T>(string collection)
        {
            MongoCollection<BsonDocument> all = this.Database.GetCollection(collection);

            return all.FindAllAs<T>();
        }
    }
}
