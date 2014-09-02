namespace SexStore.MongoServer.Data.Imports.Strategies
{
    using System.Collections.Generic;
    using System.IO;
    using Client.Readers;
    using Client.Readers.HelperStructures;

    public class XmlProductImport : IMongoProductImport
    {
        public XmlProductImport()
        {
        }

        public List<Product> GetProductData(string fileName)
        {
            string path = @"../../../SexStore.MongoServer.Data/ExternalData/XML/" + fileName;

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(string.Format("{0} couldn't be found. Import aborted.", fileName));
            }

            return XMLImporter.GetProducts(path);
        }
    }
}
