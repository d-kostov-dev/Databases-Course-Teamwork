namespace SexStore.MongoServer.Data.Imports.Strategies
{
    using System.Collections.Generic;
    using Client.Readers.HelperStructures;

    public interface IMongoProductImport
    {
        List<Product> GetProductData(string fileName);
    }
}
