namespace BoVoyageMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRaisonAnnulerToDossierReservationNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DossiersReservations", "RaisonAnnulationDossier", c => c.Byte());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DossiersReservations", "RaisonAnnulationDossier", c => c.Byte(nullable: false));
        }
    }
}
