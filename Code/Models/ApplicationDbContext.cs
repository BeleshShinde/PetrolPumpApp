using System.Data.Entity;

namespace PetrolPumpApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<DispensingRecord> DispensingRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure decimal precision for QuantityFilled
            modelBuilder.Entity<DispensingRecord>()
                .Property(d => d.QuantityFilled)
                .HasPrecision(18, 2);
        }
    }
}
