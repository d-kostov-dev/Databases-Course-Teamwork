namespace SexStore.MongoServer.Data.Imports.Strategies
{
    using System.Collections.Generic;
    using Client.Readers;
    using Client.Readers.HelperStructures;

    public class XmlProductImport : IMongoProductImport
    {
        public XmlProductImport()
        {
        }

        public List<Product> GetProductData(string fileName)
        {
            return XMLImporter.GetProducts(@"../../../SexStore.MongoServer.Data/ExternalData/XML/" + fileName);
        }
    }
}
