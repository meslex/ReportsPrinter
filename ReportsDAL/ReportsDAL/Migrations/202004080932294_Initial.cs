namespace ReportsDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AmountOfCreatedReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractId = c.Int(nullable: false),
                        GrantAgreementId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: true)
                .ForeignKey("dbo.GrantAgreements", t => t.GrantAgreementId, cascadeDelete: true)
                .Index(t => t.ContractId)
                .Index(t => t.GrantAgreementId);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        ExecutorId = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Executors", t => t.ExecutorId, cascadeDelete: true)
                .Index(t => t.ExecutorId);
            
            CreateTable(
                "dbo.Executors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Patronymic = c.String(maxLength: 50),
                        Birthday = c.DateTime(nullable: false),
                        TIN = c.String(maxLength: 12),
                        PassportNumber = c.String(maxLength: 8),
                        PassportDateOfIssue = c.DateTime(nullable: false),
                        Entrepreneur = c.Boolean(nullable: false),
                        PassportIssuedBy = c.String(maxLength: 100),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 50),
                        ExecutorId = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Executors", t => t.ExecutorId, cascadeDelete: true)
                .Index(t => t.ExecutorId);
            
            CreateTable(
                "dbo.GrantAgreements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 12),
                        Name = c.String(maxLength: 150),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Directions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 12),
                        Name = c.String(maxLength: 150),
                        GrantAgreementId = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GrantAgreements", t => t.GrantAgreementId, cascadeDelete: true)
                .Index(t => t.GrantAgreementId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Type = c.String(maxLength: 50),
                        Price = c.Single(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
            CreateTable(
                "dbo.DirectionServices",
                c => new
                    {
                        Direction_Id = c.Int(nullable: false),
                        Service_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Direction_Id, t.Service_Id })
                .ForeignKey("dbo.Directions", t => t.Direction_Id, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.Service_Id, cascadeDelete: true)
                .Index(t => t.Direction_Id)
                .Index(t => t.Service_Id);
            
            CreateTable(
                "dbo.ContractGrantAgreements",
                c => new
                    {
                        Contract_Id = c.Int(nullable: false),
                        GrantAgreement_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Contract_Id, t.GrantAgreement_Id })
                .ForeignKey("dbo.Contracts", t => t.Contract_Id, cascadeDelete: true)
                .ForeignKey("dbo.GrantAgreements", t => t.GrantAgreement_Id, cascadeDelete: true)
                .Index(t => t.Contract_Id)
                .Index(t => t.GrantAgreement_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AmountOfCreatedReports", "GrantAgreementId", "dbo.GrantAgreements");
            DropForeignKey("dbo.ContractGrantAgreements", "GrantAgreement_Id", "dbo.GrantAgreements");
            DropForeignKey("dbo.ContractGrantAgreements", "Contract_Id", "dbo.Contracts");
            DropForeignKey("dbo.Directions", "GrantAgreementId", "dbo.GrantAgreements");
            DropForeignKey("dbo.DirectionServices", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.DirectionServices", "Direction_Id", "dbo.Directions");
            DropForeignKey("dbo.Contracts", "ExecutorId", "dbo.Executors");
            DropForeignKey("dbo.Clients", "ExecutorId", "dbo.Executors");
            DropForeignKey("dbo.AmountOfCreatedReports", "ContractId", "dbo.Contracts");
            DropIndex("dbo.ContractGrantAgreements", new[] { "GrantAgreement_Id" });
            DropIndex("dbo.ContractGrantAgreements", new[] { "Contract_Id" });
            DropIndex("dbo.DirectionServices", new[] { "Service_Id" });
            DropIndex("dbo.DirectionServices", new[] { "Direction_Id" });
            DropIndex("dbo.Directions", new[] { "GrantAgreementId" });
            DropIndex("dbo.Clients", new[] { "ExecutorId" });
            DropIndex("dbo.Contracts", new[] { "ExecutorId" });
            DropIndex("dbo.AmountOfCreatedReports", new[] { "GrantAgreementId" });
            DropIndex("dbo.AmountOfCreatedReports", new[] { "ContractId" });
            DropTable("dbo.ContractGrantAgreements");
            DropTable("dbo.DirectionServices");
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Services");
            DropTable("dbo.Directions");
            DropTable("dbo.GrantAgreements");
            DropTable("dbo.Clients");
            DropTable("dbo.Executors");
            DropTable("dbo.Contracts");
            DropTable("dbo.AmountOfCreatedReports");
        }
    }
}
