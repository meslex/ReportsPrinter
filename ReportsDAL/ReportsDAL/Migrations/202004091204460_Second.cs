namespace ReportsDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GrantAgreements", "From", c => c.DateTime(nullable: false));
            AddColumn("dbo.GrantAgreements", "To", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GrantAgreements", "To");
            DropColumn("dbo.GrantAgreements", "From");
        }
    }
}
