namespace ReportsDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable("dbo.Executors",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FirstName = c.String(maxLength: 50),
                    LastName = c.String(maxLength: 50),
                    Patronymic = c.String(maxLength: 50),
                    TIN = c.String(maxLength: 12),
                    PassportNumber = c.String(maxLength: 8),
                    PassportDateOfIssue = c.DateTime(nullable: false),
                    PassportIssuedBy = c.String(maxLength: 100),
                    Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                })
                    .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 50),
                        ExecutorId = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Executors", t => t.ExecutorId, cascadeDelete: true)
                .Index(t => t.ExecutorId);
            

            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "ExecutorId", "dbo.Executors");
            DropIndex("dbo.Clients", new[] { "ExecutorId" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Services");
            DropTable("dbo.Executors");
            DropTable("dbo.Clients");
        }
    }
}
