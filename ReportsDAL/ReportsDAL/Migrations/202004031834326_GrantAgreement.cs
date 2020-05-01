namespace ReportsDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GrantAgreement : DbMigration
    {
        public override void Up()
        {
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
                "dbo.GrantAgreements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 12),
                        Name = c.String(maxLength: 150),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Services", "DirectionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Services", "DirectionId");
            AddForeignKey("dbo.Services", "DirectionId", "dbo.Directions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "DirectionId", "dbo.Directions");
            DropForeignKey("dbo.Directions", "GrantAgreementId", "dbo.GrantAgreements");
            DropIndex("dbo.Services", new[] { "DirectionId" });
            DropIndex("dbo.Directions", new[] { "GrantAgreementId" });
            DropColumn("dbo.Services", "DirectionId");
            DropTable("dbo.GrantAgreements");
            DropTable("dbo.Directions");
        }
    }
}
