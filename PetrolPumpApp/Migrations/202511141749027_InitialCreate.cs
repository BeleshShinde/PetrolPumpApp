namespace PetrolPumpApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DispensingRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DispenserNo = c.String(nullable: false, maxLength: 50),
                        QuantityFilled = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VehicleNumber = c.String(nullable: false, maxLength: 50),
                        PaymentMode = c.String(nullable: false, maxLength: 50),
                        PaymentProofPath = c.String(maxLength: 500),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DispensingRecords");
        }
    }
}
