namespace SQLiteServer.Data
{
    using System.Collections.Generic;
    using System.Data.SQLite;

    public class SQLiteServer
    {
        private SQLiteConnection sqLiteConnection;

        public SQLiteServer(string connectionString)
        {
            this.sqLiteConnection = new SQLiteConnection(connectionString);
        }

        public ICollection<ProductInfo> GetProductsInformation()
        {
            var reportsToReturn = new List<ProductInfo>();

            this.sqLiteConnection.Open();

            using (sqLiteConnection)
            {
                string sqlCommand = "SELECT * FROM productsInfo";
                var commandToExecute = new SQLiteCommand(sqlCommand, this.sqLiteConnection);
                var reader = commandToExecute.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        string productName = (string)reader["product_name"];
                        int productTax = (int)reader["roduct_tax"];
                        var currentProductInfo = new ProductInfo(productName, productTax);
                        reportsToReturn.Add(currentProductInfo);
                    }
                }
            }

            return reportsToReturn;
        }
    }
}
