namespace OrangeApartments.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_auth : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApartmentComments", "CommentDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Apartments", "City", c => c.String(maxLength: 50));
            AlterColumn("dbo.Apartments", "Street", c => c.String(maxLength: 50));
            AlterColumn("dbo.Apartments", "StreetNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Name", c => c.String());
            AlterColumn("dbo.Users", "Phone", c => c.String());
            AlterColumn("dbo.Users", "Mail", c => c.String());
            AlterColumn("dbo.Chats", "MessageDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserComments", "CommentDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserComments", "CommentDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Chats", "MessageDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "Mail", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Phone", c => c.String(maxLength: 10));
            AlterColumn("dbo.Users", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Apartments", "StreetNumber", c => c.Short(nullable: false));
            AlterColumn("dbo.Apartments", "Street", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Apartments", "City", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ApartmentComments", "CommentDate", c => c.DateTime(nullable: false));
        }
    }
}
