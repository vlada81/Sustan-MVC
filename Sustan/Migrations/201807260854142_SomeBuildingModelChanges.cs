namespace Sustan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeBuildingModelChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Buildings", "Entrance", c => c.String(maxLength: 10));
            AlterColumn("dbo.Buildings", "ParcelNumber", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Buildings", "ParcelNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Buildings", "Entrance", c => c.String());
        }
    }
}
