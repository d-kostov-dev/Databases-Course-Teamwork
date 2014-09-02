namespace SexStore.MongoServer.Data.Imports
{
    using System.Collections.Generic;
    using System.Linq;
    using Client.Readers.HelperStructures;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using Strategies;
    using Model = MongoServer.Models;

    public sealed class MongoProductImporter
    {
        private readonly IList<Product> products;
        private IMongoProductImport importStrategy;

        public MongoProductImporter(ImportType importType, string fileName, MongoDatabase database)
        {
            this.Database = database;
            this.AssignImportStrategy(importType);
            this.products = this.importStrategy.GetProductData(fileName);
        }

        public MongoDatabase Database { get; private set; }

        public bool InitializeImport()
        {
            HashSet<Model.ProductType> types = this.GetAllNeededProductTypes();
            Dictionary<string, ICollection<ObjectId>> shops = this.GetAllNeededShops();
            Dictionary<Model.Product, ICollection<string>> generatedProducts = this.GenerateProductModels(types);

            IEnumerable<WriteConcernResult> result =
                this.Database.GetCollection("Products").InsertBatch<Model.Product>(generatedProducts.Keys);

            MongoCollection<BsonDocument> shopCollection = this.Database.GetCollection("Shops");

            //// lo6o mi e
            foreach (var shop in shops)
            {
                ICollection<ObjectId> products = shop.Value;

                foreach (var product in generatedProducts)
                {
                    foreach (string productSeenInShop in product.Value)
                    {
                        if (shop.Key == productSeenInShop)
                        {
                            products.Add(product.Key.Id);
                        }
                    }
                }

                var update = Update.Set("ProductIds", new BsonArray(products));
                var query = Query.EQ("Name", shop.Key);
                shopCollection.Update(query, update);
            }

            //// WARNING: This check might not be adequate
            foreach (WriteConcernResult import in result)
            {
                if (!import.Ok)
                {
                    return false;
                }
            }

            return true;
        }

        private Dictionary<Model.Product, ICollection<string>> GenerateProductModels(ISet<Model.ProductType> types)
        {
            Dictionary<Model.Product, ICollection<string>> models = 
                new Dictionary<Model.Product, ICollection<string>>();

            foreach (Product product in this.products)
            {
                ObjectId typeId = types.Where(t => t.Name == product.Type).Select(t => t.Id).First();

                Model.Product currentProduct = new Model.Product(
                    product.Name,
                    product.Description,
                    product.ProductCode,
                    product.Price,
                    product.Quantity,
                    typeId,
                    product.CategoryIds);

                models.Add(currentProduct, product.Shops);
            }

            return models;
        }

        private HashSet<Model.ProductType> GetAllNeededProductTypes()
        {
            HashSet<string> typeNames = new HashSet<string>();
            foreach (Product product in this.products)
            {
                typeNames.Add(product.Type);
            }

            var query = Query.In("Name", new BsonArray(typeNames));

            MongoCursor<Model.ProductType> typesCursor =
            this.Database.GetCollection("ProductTypes")
                .FindAs<Model.ProductType>(query);

            HashSet<Model.ProductType> types = new HashSet<Model.ProductType>();
            foreach (Model.ProductType type in typesCursor)
            {
                types.Add(type);
            }

            return types;
        }

        private Dictionary<string, ICollection<ObjectId>> GetAllNeededShops()
        {
            HashSet<string> shopNames = new HashSet<string>();
            foreach (Product product in this.products)
            {
                foreach (string shopName in product.Shops)
                {
                    shopNames.Add(shopName);
                }
            }

            var query = Query.In("Name", new BsonArray(shopNames));

            MongoCursor<Model.Shop> shopsCursor =
            this.Database.GetCollection("Shops")
                .FindAs<Model.Shop>(query);

            Dictionary<string, ICollection<ObjectId>> shops = 
                new Dictionary<string, ICollection<ObjectId>>();
            foreach (Model.Shop shop in shopsCursor)
            {
                shops.Add(shop.Name, shop.ProductIds);
            }

            return shops;
        }

        private void AssignImportStrategy(ImportType importType)
        {
            switch (importType)
            {
                case ImportType.XML:
                    this.importStrategy = new XmlProductImport();
                    break;
            }
        }
    }
}
