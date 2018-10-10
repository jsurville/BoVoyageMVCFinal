namespace BoVoyageMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageCommercialAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        ContentType = c.String(nullable: false, maxLength: 20),
                        Content = c.Binary(nullable: false),
                        DestinationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Destinations", t => t.DestinationId, cascadeDelete: false)
                .Index(t => t.DestinationId);
            
            CreateTable(
                "dbo.Commercials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Title = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 30),
                        FisrtName = c.String(nullable: false, maxLength: 30),
                        Address = c.String(nullable: false, maxLength: 60),
                        PhoneNumber = c.String(nullable: false, maxLength: 20),
                        BirthDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => new { t.Title, t.LastName, t.FisrtName, t.Address }, unique: true, name: "IX_PersonneUnique");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Commercials", "UserId", "dbo.Users");
            DropForeignKey("dbo.Images", "DestinationId", "dbo.Destinations");
            DropIndex("dbo.Commercials", "IX_PersonneUnique");
            DropIndex("dbo.Commercials", new[] { "UserId" });
            DropIndex("dbo.Images", new[] { "DestinationId" });
            DropTable("dbo.Commercials");
            DropTable("dbo.Images");
        }
    }
}
