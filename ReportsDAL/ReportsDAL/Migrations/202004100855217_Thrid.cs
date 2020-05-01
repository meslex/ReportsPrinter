namespace ReportsDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Thrid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DirectionServices", "Direction_Id", "dbo.Directions");
            DropForeignKey("dbo.DirectionServices", "Service_Id", "dbo.Services");
            DropIndex("dbo.DirectionServices", new[] { "Direction_Id" });
            DropIndex("dbo.DirectionServices", new[] { "Service_Id" });
            AddColumn("dbo.Services", "DirectionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Services", "DirectionId");
            AddForeignKey("dbo.Services", "DirectionId", "dbo.Directions", "Id", cascadeDelete: true);
            DropTable("dbo.DirectionServices");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DirectionServices",
                c => new
                    {
                        Direction_Id = c.Int(nullable: false),
                        Service_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Direction_Id, t.Service_Id });
            
            DropForeignKey("dbo.Services", "DirectionId", "dbo.Directions");
            DropIndex("dbo.Services", new[] { "DirectionId" });
            DropColumn("dbo.Services", "DirectionId");
            CreateIndex("dbo.DirectionServices", "Service_Id");
            CreateIndex("dbo.DirectionServices", "Direction_Id");
            AddForeignKey("dbo.DirectionServices", "Service_Id", "dbo.Services", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DirectionServices", "Direction_Id", "dbo.Directions", "Id", cascadeDelete: true);
        }
    }
}
