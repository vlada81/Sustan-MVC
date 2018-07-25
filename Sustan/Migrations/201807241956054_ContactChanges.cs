namespace Sustan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "PhoneNumber", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "PhoneNumber", c => c.String());
        }
    }
}
