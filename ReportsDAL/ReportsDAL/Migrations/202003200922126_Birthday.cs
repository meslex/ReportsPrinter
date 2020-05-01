namespace ReportsDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Birthday : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Executors", "Birthday", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Executors", "Birthday");
        }
    }
}
