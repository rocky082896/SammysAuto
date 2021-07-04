namespace Teleperformance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_outbound : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_outbound", "inbound_id", c => c.Int(nullable: false));
            DropColumn("dbo.tbl_outbound", "tag_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_outbound", "tag_id", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.tbl_outbound", "inbound_id");
        }
    }
}
