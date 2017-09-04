namespace OrangeApartments.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrangeMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apartments", "BedroomCount", c => c.Short(nullable: false));
            AddColumn("dbo.Apartments", "SleepingPlaces", c => c.Short(nullable: false));
            AddColumn("dbo.Apartments", "RentType", c => c.Short(nullable: false));
            AddColumn("dbo.Apartments", "StreetNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Apartments", "FloorNumber", c => c.Short(nullable: false));
            AlterColumn("dbo.Apartments", "PostDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Apartments", "PostDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Apartments", "FloorNumber");
            DropColumn("dbo.Apartments", "StreetNumber");
            DropColumn("dbo.Apartments", "RentType");
            DropColumn("dbo.Apartments", "SleepingPlaces");
            DropColumn("dbo.Apartments", "BedroomCount");
        }
    }
}
