namespace BoVoyageMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredUserIdFroCommercial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Commercials", "UserId", "dbo.Users");
            DropIndex("dbo.Commercials", new[] { "UserId" });
            AlterColumn("dbo.Commercials", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Commercials", "UserId");
            AddForeignKey("dbo.Commercials", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Commercials", "UserId", "dbo.Users");
            DropIndex("dbo.Commercials", new[] { "UserId" });
            AlterColumn("dbo.Commercials", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Commercials", "UserId");
            AddForeignKey("dbo.Commercials", "UserId", "dbo.Users", "Id");
        }
    }
}
