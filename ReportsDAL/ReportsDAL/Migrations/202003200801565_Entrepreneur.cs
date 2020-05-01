namespace ReportsDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Entrepreneur : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Executors", "Entrepreneur", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Executors", "Entrepreneur");
        }
    }
}
