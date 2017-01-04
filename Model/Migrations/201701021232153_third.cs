namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AdvertisementImage", new[] { "Advertisement_Id" });
            RenameColumn(table: "dbo.AdvertisementImage", name: "Advertisement_Id", newName: "AdvertisementId");
            AlterColumn("dbo.AdvertisementImage", "AdvertisementId", c => c.Int(nullable: false));
            CreateIndex("dbo.AdvertisementImage", "AdvertisementId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AdvertisementImage", new[] { "AdvertisementId" });
            AlterColumn("dbo.AdvertisementImage", "AdvertisementId", c => c.Int());
            RenameColumn(table: "dbo.AdvertisementImage", name: "AdvertisementId", newName: "Advertisement_Id");
            CreateIndex("dbo.AdvertisementImage", "Advertisement_Id");
        }
    }
}
