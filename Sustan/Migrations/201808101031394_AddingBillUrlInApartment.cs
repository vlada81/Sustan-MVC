namespace Sustan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingBillUrlInApartment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apartments", "ApartmentBillUrl", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Apartments", "ApartmentBillUrl");
        }
    }
}
