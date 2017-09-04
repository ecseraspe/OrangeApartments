namespace OrangeApartments.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orange2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Name", c => c.String());
            AlterColumn("dbo.Users", "Phone", c => c.String());
            AlterColumn("dbo.Users", "Mail", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Mail", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Users", "Phone", c => c.String(maxLength: 10));
            AlterColumn("dbo.Users", "Name", c => c.String(maxLength: 50));
        }
    }
}
