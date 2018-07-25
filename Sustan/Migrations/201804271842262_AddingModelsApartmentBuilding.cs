namespace Sustan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingModelsApartmentBuilding : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Apartments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JIBS = c.String(nullable: false),
                        ApartmentNumber = c.Int(nullable: false),
                        ApartmentArea = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumberOfTenants = c.Int(nullable: false),
                        CostOfService = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuildingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: true)
                .Index(t => t.BuildingId);
            
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JIBZ = c.String(nullable: false),
                        Street = c.String(nullable: false, maxLength: 100),
                        Number = c.Int(nullable: false),
                        Entrance = c.String(),
                        NumberOfFloors = c.Int(nullable: false),
                        Pib = c.Double(nullable: false),
                        BuildingRegistrationNumber = c.Double(nullable: false),
                        AccountNumber = c.String(nullable: false),
                        AccountBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ParcelNumber = c.String(nullable: false),
                        BuildingArea = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuildingManager = c.String(nullable: false, maxLength: 100),
                        ImageUrl = c.String(maxLength: 1024),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Apartments", "BuildingId", "dbo.Buildings");
            DropIndex("dbo.Apartments", new[] { "BuildingId" });
            DropTable("dbo.Buildings");
            DropTable("dbo.Apartments");
        }
    }
}
