namespace ReportsDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class History1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reports", "GrantAgreementNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reports", "GrantAgreementNumber", c => c.Int(nullable: false));
        }
    }
}
