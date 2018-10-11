namespace BoVoyageMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDestination2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clients", "UserId", "dbo.Users");
            DropIndex("dbo.Clients", new[] { "UserId" });
            AlterColumn("dbo.Clients", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Clients", "UserId");
            AddForeignKey("dbo.Clients", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "UserId", "dbo.Users");
            DropIndex("dbo.Clients", new[] { "UserId" });
            AlterColumn("dbo.Clients", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Clients", "UserId");
            AddForeignKey("dbo.Clients", "UserId", "dbo.Users", "Id");
        }
    }
}
