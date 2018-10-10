namespace BoVoyageMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgencesVoyages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Voyages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartureDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        MaxCapacity = c.Int(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        Margin = c.Double(nullable: false),
                        AgenceVoyageId = c.Int(nullable: false),
                        DestinationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AgencesVoyages", t => t.AgenceVoyageId, cascadeDelete: false)
                .ForeignKey("dbo.Destinations", t => t.DestinationId, cascadeDelete: false)
                .Index(t => new { t.DepartureDate, t.ReturnDate, t.AgenceVoyageId, t.DestinationId }, unique: true, name: "IX_DatesAgenceDestination");
            
            CreateTable(
                "dbo.Destinations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Continent = c.String(nullable: false, maxLength: 40),
                        Country = c.String(nullable: false, maxLength: 40),
                        Region = c.String(nullable: false, maxLength: 40),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.Continent, t.Country, t.Region }, unique: true, name: "IX_ContinentPaysRegion");
            
            CreateTable(
                "dbo.DossiersReservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreditCardNumber = c.String(nullable: false, maxLength: 20),
                        UnitPrice = c.Double(nullable: false),
                        EtatDossier = c.Byte(nullable: false),
                        ClientId = c.Int(nullable: false),
                        VoyageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: false)
                .ForeignKey("dbo.Voyages", t => t.VoyageId, cascadeDelete: false)
                .Index(t => t.ClientId)
                .Index(t => t.VoyageId);
            
            CreateTable(
                "dbo.Assurances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Montant = c.Decimal(nullable: false, storeType: "money"),
                        TypeAssurance = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => new { t.Montant, t.TypeAssurance }, unique: true, name: "IX_MontantType");
            
            CreateTable(
                "dbo.Clients",
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
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DossierReservationId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 30),
                        FisrtName = c.String(nullable: false, maxLength: 30),
                        Address = c.String(nullable: false, maxLength: 60),
                        PhoneNumber = c.String(nullable: false, maxLength: 20),
                        BirthDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DossiersReservations", t => t.DossierReservationId, cascadeDelete: false)
                .Index(t => t.DossierReservationId)
                .Index(t => new { t.Title, t.LastName, t.FisrtName, t.Address }, unique: true, name: "IX_PersonneUnique");
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AssuranceDossierReservations",
                c => new
                    {
                        Assurance_ID = c.Int(nullable: false),
                        DossierReservation_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Assurance_ID, t.DossierReservation_Id })
                .ForeignKey("dbo.Assurances", t => t.Assurance_ID, cascadeDelete: false)
                .ForeignKey("dbo.DossiersReservations", t => t.DossierReservation_Id, cascadeDelete: false)
                .Index(t => t.Assurance_ID)
                .Index(t => t.DossierReservation_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.DossiersReservations", "VoyageId", "dbo.Voyages");
            DropForeignKey("dbo.Participants", "DossierReservationId", "dbo.DossiersReservations");
            DropForeignKey("dbo.DossiersReservations", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Clients", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.AssuranceDossierReservations", "DossierReservation_Id", "dbo.DossiersReservations");
            DropForeignKey("dbo.AssuranceDossierReservations", "Assurance_ID", "dbo.Assurances");
            DropForeignKey("dbo.Voyages", "DestinationId", "dbo.Destinations");
            DropForeignKey("dbo.Voyages", "AgenceVoyageId", "dbo.AgencesVoyages");
            DropIndex("dbo.AssuranceDossierReservations", new[] { "DossierReservation_Id" });
            DropIndex("dbo.AssuranceDossierReservations", new[] { "Assurance_ID" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.Participants", "IX_PersonneUnique");
            DropIndex("dbo.Participants", new[] { "DossierReservationId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Clients", "IX_PersonneUnique");
            DropIndex("dbo.Clients", new[] { "UserId" });
            DropIndex("dbo.Assurances", "IX_MontantType");
            DropIndex("dbo.DossiersReservations", new[] { "VoyageId" });
            DropIndex("dbo.DossiersReservations", new[] { "ClientId" });
            DropIndex("dbo.Destinations", "IX_ContinentPaysRegion");
            DropIndex("dbo.Voyages", "IX_DatesAgenceDestination");
            DropIndex("dbo.AgencesVoyages", new[] { "Name" });
            DropTable("dbo.AssuranceDossierReservations");
            DropTable("dbo.Roles");
            DropTable("dbo.Participants");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Clients");
            DropTable("dbo.Assurances");
            DropTable("dbo.DossiersReservations");
            DropTable("dbo.Destinations");
            DropTable("dbo.Voyages");
            DropTable("dbo.AgencesVoyages");
        }
    }
}
