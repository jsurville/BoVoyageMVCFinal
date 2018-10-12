namespace BoVoyageMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRaisonAnnulerToDossierReservation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DossiersReservations", "RaisonAnnulationDossier", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DossiersReservations", "RaisonAnnulationDossier");
        }
    }
}
