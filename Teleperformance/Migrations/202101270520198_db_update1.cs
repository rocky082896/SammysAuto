namespace Teleperformance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db_update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_inbound", "asset_no", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.tbl_inbound", "asset_desc", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_inbound", "asset_desc");
            DropColumn("dbo.tbl_inbound", "asset_no");
        }
    }
}
