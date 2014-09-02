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
        private IMongoProductImport importStrategy;
        private readonly IList<Product> products;

        public MongoProductImporter(ImportType importType, string fileName, MongoDatabase database)
        {
            this.Database = database;
            this.AssignImportStrategy(importType);
            this.products = this.importStrategy.GetProductData(fileName);
        }

        public MongoDatabase Database { get; private set; }

        public void InitializeImport()
        {
            HashSet<Model.ProductType> types = this.GetAllProductTypes();
            HashSet<Model.Product> generatedProducts = this.GenerateProductModels(types);

            this.Database.GetCollection("Products").InsertBatch<Model.Product>(generatedProducts);
        }

        private HashSet<Model.Product> GenerateProductModels(ISet<Model.ProductType> types)
        {
            HashSet<Model.Product> models = new HashSet<Model.Product>();

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

                models.Add(currentProduct);
            }

            return models;
        }

        private HashSet<Model.ProductType> GetAllProductTypes()
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
