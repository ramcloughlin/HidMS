namespace hidMy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GLaccounts",
                c => new
                    {
                        Account = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150, fixedLength: true),
                        ClauseText = c.String(),
                        NonComplianceText = c.String(),
                        RemedialActionText = c.String(),
                        hid = c.Binary(maxLength: 892),
                        Level = c.Short(),
                        parent = c.Long(),
                    })
                .PrimaryKey(t => t.Account);
            
            CreateTable(
                "dbo.GLAspectsandElements",
                c => new
                    {
                        AspectsElementsID = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        CommentLINK = c.Int(),
                        LeglislationLINK = c.Long(),
                        hid = c.Binary(maxLength: 892),
                        Level = c.Short(),
                        parent = c.Long(),
                    })
                .PrimaryKey(t => t.AspectsElementsID)
                .ForeignKey("dbo.GLaccounts", t => t.LeglislationLINK)
                .Index(t => t.LeglislationLINK);
            
            CreateTable(
                "dbo.GLLeglislation",
                c => new
                    {
                        LeglislationID = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        ClauseText = c.String(),
                        NonComplianceText = c.String(),
                        RemedialActionText = c.String(),
                        hid = c.Binary(maxLength: 892),
                        Level = c.Short(),
                        parent = c.Long(),
                        GLAspectsandElements_AspectsElementsID = c.Long(),
                    })
                .PrimaryKey(t => t.LeglislationID)
                .ForeignKey("dbo.GLAspectsandElements", t => t.GLAspectsandElements_AspectsElementsID)
                .Index(t => t.GLAspectsandElements_AspectsElementsID);
            
            CreateTable(
                "dbo.InspectionDetails",
                c => new
                    {
                        InspectionNo = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        PremisesRef = c.Int(),
                        MLRQ1 = c.Boolean(nullable: false),
                        MLRQ2 = c.Boolean(nullable: false),
                        MLRQ3 = c.Boolean(nullable: false),
                        MLRQ4 = c.Boolean(nullable: false),
                        officer = c.String(maxLength: 200),
                        GeneralHygieneStatus = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.InspectionNo)
                .ForeignKey("dbo.NationalPremisesRegister", t => t.PremisesRef)
                .Index(t => t.PremisesRef);
            
            CreateTable(
                "dbo.NationalPremisesRegister",
                c => new
                    {
                        Reference = c.Int(nullable: false),
                        Area = c.String(maxLength: 255),
                        SubArea = c.String(maxLength: 255),
                        Name = c.String(maxLength: 255),
                        Telephone = c.String(maxLength: 255),
                        SubBuildingNumber = c.String(maxLength: 255),
                        BuildingName = c.String(maxLength: 255),
                        BuildingNumber = c.String(maxLength: 255),
                        StreetName = c.String(maxLength: 255),
                        Townland = c.String(maxLength: 255),
                        Locality = c.String(maxLength: 255),
                        CityTown = c.String(maxLength: 255),
                        County = c.String(maxLength: 255),
                        Postcode = c.String(maxLength: 255),
                        FoodBusinessOperators = c.String(maxLength: 255),
                        _852Registered = c.String(name: "852Registered", maxLength: 255),
                        _852Unregistered = c.String(name: "852Unregistered", maxLength: 255),
                        _853Approved = c.String(name: "853Approved", maxLength: 255),
                        BusinessCategory = c.String(maxLength: 255),
                        BusinessType = c.String(maxLength: 255),
                        RiskCategory = c.Int(),
                        LastInspection = c.DateTime(),
                        LastPlannedInspection = c.DateTime(),
                        LastPlannedSurveillanceInspection = c.DateTime(),
                        NextPlannedPlannedSurveillance = c.DateTime(name: "NextPlanned Planned Surveillance"),
                        NFL = c.String(maxLength: 255),
                        OOJ = c.String(maxLength: 255),
                        SRM = c.String(maxLength: 255),
                        MLR = c.String(maxLength: 255),
                        FoodOfficer = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Reference);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InspectionDetails", "PremisesRef", "dbo.NationalPremisesRegister");
            DropForeignKey("dbo.GLLeglislation", "GLAspectsandElements_AspectsElementsID", "dbo.GLAspectsandElements");
            DropForeignKey("dbo.GLAspectsandElements", "LeglislationLINK", "dbo.GLaccounts");
            DropIndex("dbo.InspectionDetails", new[] { "PremisesRef" });
            DropIndex("dbo.GLLeglislation", new[] { "GLAspectsandElements_AspectsElementsID" });
            DropIndex("dbo.GLAspectsandElements", new[] { "LeglislationLINK" });
            DropTable("dbo.NationalPremisesRegister");
            DropTable("dbo.InspectionDetails");
            DropTable("dbo.GLLeglislation");
            DropTable("dbo.GLAspectsandElements");
            DropTable("dbo.GLaccounts");
        }
    }
}
