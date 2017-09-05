namespace OrangeApartments.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Name", c => c.String());
            AlterColumn("dbo.Users", "Phone", c => c.String());
            AlterColumn("dbo.Tags", "TagName", c => c.String(nullable: false, maxLength: 25));
            CreateIndex("dbo.Tags", "TagName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tags", new[] { "TagName" });
            AlterColumn("dbo.Tags", "TagName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Users", "Phone", c => c.String(maxLength: 10));
            AlterColumn("dbo.Users", "Name", c => c.String(maxLength: 50));
        }
    }
}
