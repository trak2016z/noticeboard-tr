namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AdvertisementImage", "Advertisement_Id", "dbo.Advertisement");
            DropIndex("dbo.AdvertisementImage", new[] { "Advertisement_Id" });
            AddColumn("dbo.AdvertisementImage", "Advertisement_Id1", c => c.Int());
            AlterColumn("dbo.AdvertisementImage", "Advertisement_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.AdvertisementImage", "Advertisement_Id1");
            AddForeignKey("dbo.AdvertisementImage", "Advertisement_Id1", "dbo.Advertisement", "Id");
            DropColumn("dbo.AdvertisementImage", "Advertidement_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdvertisementImage", "Advertidement_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.AdvertisementImage", "Advertisement_Id1", "dbo.Advertisement");
            DropIndex("dbo.AdvertisementImage", new[] { "Advertisement_Id1" });
            AlterColumn("dbo.AdvertisementImage", "Advertisement_Id", c => c.Int());
            DropColumn("dbo.AdvertisementImage", "Advertisement_Id1");
            CreateIndex("dbo.AdvertisementImage", "Advertisement_Id");
            AddForeignKey("dbo.AdvertisementImage", "Advertisement_Id", "dbo.Advertisement", "Id");
        }
    }
}
