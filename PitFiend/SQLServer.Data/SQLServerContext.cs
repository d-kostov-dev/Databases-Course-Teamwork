namespace SQLServer.Data
{
    using System.Data.Entity;
    using SexStore.Models;

    public class SQLServerContext : DbContext
    {
        
        public SQLServerContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Shop> Shops { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }
    }
}
