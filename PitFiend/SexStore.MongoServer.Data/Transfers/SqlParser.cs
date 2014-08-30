﻿namespace SexStore.MongoServer.Data.Transfers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using SQLServer.Data;
    using Mongo = MongoServer.Models;
    using SQL = SexStore.Models;

    public sealed class SqlParser
    {
        public SqlParser(MongoDatabase database, SQLServerContext sqlDbContext)
        {
            this.Database = database;
            this.SqlDbContext = sqlDbContext;
            this.IsDataParsed = false;
        }

        private MongoDatabase Database { get; set; }

        private SQLServerContext SqlDbContext { get; set; }

        private bool IsDataParsed { get; set; }

        private IDictionary<ObjectId, SQL.City> ParsedCities { get; set; }

        private IDictionary<ObjectId, SQL.ProductType> ParsedTypes { get; set; }

        private IDictionary<ObjectId, SQL.Product> ParsedProducts { get; set; }

        private ISet<SQL.Shop> ParsedShops { get; set; }

        public void InitializeParsing()
        {
            try
            {
                this.ParseProductTypesToSql();
                this.ParseCitiesToSql();
                this.ParseProductsToSql();
                this.ParseShopsToSql();

                this.IsDataParsed = true;
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine("KeyNotFoundException: {0}\n\n{1}", ex.Message, ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}\n\n{1}", ex.Message, ex.StackTrace);
            }
        }

        //// Get methods

        public ICollection<SQL.ProductType> GetProductTypes()
        {
            this.ValidateIfDataIsParsed();

            return this.ParsedTypes.Values;
        }

        public ICollection<SQL.City> GetCities()
        {
            this.ValidateIfDataIsParsed();

            return this.ParsedCities.Values;
        }

        public ICollection<SQL.Product> GetProducts()
        {
            this.ValidateIfDataIsParsed();

            return this.ParsedProducts.Values;
        }

        public ICollection<SQL.Shop> GetShops()
        {
            this.ValidateIfDataIsParsed();

            return this.ParsedShops;
        }

        //// Parse methods

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
                IQueryable<SQL.Category> categoriesQuery = this.GetSqlCategories(product.CategoryIds);

                if (!this.ParsedTypes.TryGetValue(currentTypeId, out currentProductType))
                {
                    throw new KeyNotFoundException(
                        "The provided ObjectId of IDictionary<ObjectId, SQL.ProductType> couldn't" +
                        "be found. No SQL.ProductType is returned. SQL.Product parsing aborted.");
                }

                SQL.Product current = new SQL.Product()
                {
                    ProductCode = product.ProductCode,
                    Name = product.Name,
                    Description = product.Description,
                    Price = (double)product.Price,
                    Type = currentProductType,
                    QuantityInStock = product.UnitsInStock,
                    Categories = categoriesQuery.ToList<SQL.Category>() //// Keep an eye on this !!!
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
                    throw new KeyNotFoundException(
                        "The provided ObjectId of IDictionary<ObjectId, SQL.City> couldn't" +
                        "be found. No SQL.City is returned. SQL.Shop parsing aborted.");
                }

                HashSet<SQL.Product> currentProducts = new HashSet<SQL.Product>();

                foreach (ObjectId productId in shop.ProductIds)
                {
                    SQL.Product currentProduct;
                    
                    if (!this.ParsedProducts.TryGetValue(productId, out currentProduct))
                    {
                        throw new KeyNotFoundException(
                            "The provided ObjectId of IDictionary<ObjectId, SQL.Product> couldn't" +
                            "be found. No SQL.Product is returned. SQL.Shop parsing aborted.");
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

        /// <summary>
        /// Gets Entity models of all available SQL categories
        /// </summary>
        /// <param name="categoryIds">ICollection<int> with all category IDs</param>
        /// <returns>Returns IQueryable<SQL.Category> with the selected categories.</returns>
        private IQueryable<SQL.Category> GetSqlCategories(ICollection<int> categoryIds)
        {
            return this.SqlDbContext.Categories.Where(cat => categoryIds.Contains(cat.ID));
        }

        /// <summary>
        /// Gets MongoCursor of a selected collection.
        /// </summary>
        /// <typeparam name="T">MongoServer.Model type</typeparam>
        /// <param name="collection">MongoDB collection name</param>
        /// <returns>Returns MongoCursor<<typeparamref name="T"/>> refering to all documents.</returns>
        private MongoCursor<T> GetCursor<T>(string collection)
        {
            MongoCollection<BsonDocument> all = this.Database.GetCollection(collection);

            return all.FindAllAs<T>();
        }

        private void ValidateIfDataIsParsed()
        {
            if (!this.IsDataParsed)
            {
                throw new InvalidOperationException("The InitializeParsing() method is not executed yet! Cannot return data.");
            }
        }
    }
}
