namespace Teleperformance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_inbound", "status", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_outbound", "status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_outbound", "status");
            DropColumn("dbo.tbl_inbound", "status");
        }
    }
}
