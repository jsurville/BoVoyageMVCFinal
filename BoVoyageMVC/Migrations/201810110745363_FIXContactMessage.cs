namespace BoVoyageMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FIXContactMessage : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ContactMessages", "IX_PersonneUnique");
        }
        
        public override void Down()
        {
            CreateIndex("dbo.ContactMessages", new[] { "Title", "LastName", "FisrtName" }, unique: true, name: "IX_PersonneUnique");
        }
    }
}
