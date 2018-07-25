namespace Sustan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserIdToApartment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apartments", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Apartments", "UserId");
            AddForeignKey("dbo.Apartments", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Apartments", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Apartments", new[] { "UserId" });
            DropColumn("dbo.Apartments", "UserId");
        }
    }
}
