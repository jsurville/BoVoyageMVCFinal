namespace BoVoyageMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addContactMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 30),
                        FisrtName = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(maxLength: 20),
                        SendDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.Title, t.LastName, t.FisrtName }, unique: true, name: "IX_PersonneUnique");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ContactMessages", "IX_PersonneUnique");
            DropTable("dbo.ContactMessages");
        }
    }
}
