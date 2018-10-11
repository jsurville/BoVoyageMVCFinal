namespace BoVoyageMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDestination : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Voyages", "Margin", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Voyages", "Margin", c => c.Double(nullable: false));
        }
    }
}
