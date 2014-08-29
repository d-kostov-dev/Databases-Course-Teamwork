namespace SQLServer.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SexStore.Models;

    public sealed class Configuration : DbMigrationsConfiguration<SQLServerContext>
    {
        //// Enable-Migrations -EnableAutomaticMigrations
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SQLServerContext context)
        {
            //// This method will be called after migrating to the latest version.

            context.Cities.AddOrUpdate(new City() { ID = 1, Name = "Sofia" });
            context.Cities.AddOrUpdate(new City() { ID = 2, Name = "Plovdiv" });
            context.Cities.AddOrUpdate(new City() { ID = 3, Name = "Varna" });
            context.Cities.AddOrUpdate(new City() { ID = 4, Name = "Bourgas" });
        }
    }
}
