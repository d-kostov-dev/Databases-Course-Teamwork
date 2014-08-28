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

            context.Cities.AddOrUpdate(new City() { Name = "Sofia"});
            context.Cities.AddOrUpdate(new City() { Name = "Plovdiv" });
            context.Cities.AddOrUpdate(new City() { Name = "Varna" });
            context.Cities.AddOrUpdate(new City() { Name = "Bourgas" });
        }
    }
}
