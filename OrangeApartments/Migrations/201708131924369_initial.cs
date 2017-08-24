namespace OrangeApartments.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApartmentComments",
                c => new
                    {
                        ApartmentCommentsId = c.Int(nullable: false, identity: true),
                        Comment = c.String(maxLength: 500),
                        CommentDate = c.DateTime(nullable: false),
                        ApartmentId = c.Int(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ApartmentCommentsId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Apartments", t => t.ApartmentId, cascadeDelete: true)
                .Index(t => t.ApartmentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Apartments",
                c => new
                    {
                        ApartmentId = c.Int(nullable: false, identity: true),
                        Type = c.Byte(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Title = c.String(maxLength: 20),
                        Rating = c.Single(nullable: false),
                        Description = c.String(maxLength: 500),
                        Square = c.Single(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        City = c.String(maxLength: 50),
                        District = c.String(maxLength: 50),
                        Street = c.String(maxLength: 50),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApartmentId)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserWatchLists",
                c => new
                    {
                        UserWatchListId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        ApartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserWatchListId)
                .ForeignKey("dbo.Apartments", t => t.ApartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ApartmentId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Phone = c.String(maxLength: 10),
                        RegistrationDate = c.DateTime(nullable: false),
                        Mail = c.String(nullable: false, maxLength: 30),
                        IsAdmin = c.Boolean(nullable: false),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.ApartmentBookings",
                c => new
                    {
                        ApartmentBookingId = c.Int(nullable: false, identity: true),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                        DateBooked = c.DateTime(nullable: false),
                        UserId = c.Int(),
                        ApartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApartmentBookingId)
                .ForeignKey("dbo.Apartments", t => t.ApartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ApartmentId);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        ChatId = c.Int(nullable: false, identity: true),
                        Message = c.String(maxLength: 500),
                        MessageDate = c.DateTime(nullable: false),
                        Receiver_UserId = c.Int(),
                        Sender_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ChatId)
                .ForeignKey("dbo.Users", t => t.Receiver_UserId)
                .ForeignKey("dbo.Users", t => t.Sender_UserId)
                .Index(t => t.Receiver_UserId)
                .Index(t => t.Sender_UserId);
            
            CreateTable(
                "dbo.UserComments",
                c => new
                    {
                        UserCommentsId = c.Int(nullable: false, identity: true),
                        Comment = c.String(maxLength: 500),
                        CommentDate = c.DateTime(nullable: false),
                        CommentedUser_UserId = c.Int(),
                        Commentator_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.UserCommentsId)
                .ForeignKey("dbo.Users", t => t.CommentedUser_UserId)
                .ForeignKey("dbo.Users", t => t.Commentator_UserId)
                .Index(t => t.CommentedUser_UserId)
                .Index(t => t.Commentator_UserId);
            
            CreateTable(
                "dbo.ApartmentTags",
                c => new
                    {
                        ApartmentTagsId = c.Int(nullable: false, identity: true),
                        TagId = c.Int(nullable: false),
                        ApartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApartmentTagsId)
                .ForeignKey("dbo.Apartments", t => t.ApartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.TagId)
                .Index(t => t.ApartmentId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        TagName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApartmentTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.ApartmentTags", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.ApartmentComments", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.UserWatchLists", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserComments", "Commentator_UserId", "dbo.Users");
            DropForeignKey("dbo.UserComments", "CommentedUser_UserId", "dbo.Users");
            DropForeignKey("dbo.Chats", "Sender_UserId", "dbo.Users");
            DropForeignKey("dbo.Chats", "Receiver_UserId", "dbo.Users");
            DropForeignKey("dbo.ApartmentBookings", "UserId", "dbo.Users");
            DropForeignKey("dbo.ApartmentBookings", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.Apartments", "UserID", "dbo.Users");
            DropForeignKey("dbo.ApartmentComments", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserWatchLists", "ApartmentId", "dbo.Apartments");
            DropIndex("dbo.ApartmentTags", new[] { "ApartmentId" });
            DropIndex("dbo.ApartmentTags", new[] { "TagId" });
            DropIndex("dbo.UserComments", new[] { "Commentator_UserId" });
            DropIndex("dbo.UserComments", new[] { "CommentedUser_UserId" });
            DropIndex("dbo.Chats", new[] { "Sender_UserId" });
            DropIndex("dbo.Chats", new[] { "Receiver_UserId" });
            DropIndex("dbo.ApartmentBookings", new[] { "ApartmentId" });
            DropIndex("dbo.ApartmentBookings", new[] { "UserId" });
            DropIndex("dbo.UserWatchLists", new[] { "ApartmentId" });
            DropIndex("dbo.UserWatchLists", new[] { "UserId" });
            DropIndex("dbo.Apartments", new[] { "UserID" });
            DropIndex("dbo.ApartmentComments", new[] { "UserId" });
            DropIndex("dbo.ApartmentComments", new[] { "ApartmentId" });
            DropTable("dbo.Tags");
            DropTable("dbo.ApartmentTags");
            DropTable("dbo.UserComments");
            DropTable("dbo.Chats");
            DropTable("dbo.ApartmentBookings");
            DropTable("dbo.Users");
            DropTable("dbo.UserWatchLists");
            DropTable("dbo.Apartments");
            DropTable("dbo.ApartmentComments");
        }
    }
}
