namespace Sustan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeNullubaleChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "RegistrationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "RegistrationDate", c => c.DateTime());
        }
    }
}
