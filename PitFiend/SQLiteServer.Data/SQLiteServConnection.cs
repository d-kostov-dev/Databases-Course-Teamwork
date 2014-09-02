namespace SQLiteServer.Data
{
    using System.Collections.Generic;
    using System.Data.SQLite;

    public class SQLiteServConnection
    {
        private SQLiteConnection sqLiteConnection;

        public SQLiteServConnection(string connectionString)
        {
            this.sqLiteConnection = new SQLiteConnection(connectionString);
        }

        public ICollection<ProductInfo> GetProductsInformation()
        {
            var reportsToReturn = new List<ProductInfo>();

            this.sqLiteConnection.Open();

            using (this.sqLiteConnection)
            {
                string sqlCommand = "SELECT * FROM productsInfo";
                var commandToExecute = new SQLiteCommand(sqlCommand, this.sqLiteConnection);
                var reader = commandToExecute.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        //System.Console.WriteLine(reader["expenses"].ToString());
                        int productCode = int.Parse(reader["product_code"].ToString());
                        string productName = (string)reader["product_name"];
                        int productTax = int.Parse(reader["tax_percent"].ToString());
                        double expenses = double.Parse(reader["expenses"].ToString());
                        var currentProductInfo = new ProductInfo(productCode, productName, productTax, expenses);
                        reportsToReturn.Add(currentProductInfo);
                    }
                }
            }

            return reportsToReturn;
        }
    }
}
