namespace Sustan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_PdfFilePath_Model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PdfFilePaths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PdfFileName = c.String(maxLength: 255),
                        PdfFileUrl = c.String(maxLength: 1024),
                        BuildingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: true)
                .Index(t => t.BuildingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PdfFilePaths", "BuildingId", "dbo.Buildings");
            DropIndex("dbo.PdfFilePaths", new[] { "BuildingId" });
            DropTable("dbo.PdfFilePaths");
        }
    }
}
