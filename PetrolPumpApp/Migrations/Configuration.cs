using System.Data.Entity.Migrations;

namespace PetrolPumpApp.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<PetrolPumpApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "PetrolPumpApp.Models.ApplicationDbContext";
        }

        protected override void Seed(PetrolPumpApp.Models.ApplicationDbContext context)
        {
            // This method will be called after migrating to the latest version.
            // You can add seed data here if needed
        }
    }
}