namespace SatoImsV1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeToDouble : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_outgoing_header", "TOTAL_ITEM_AMOUNT", c => c.Double(nullable: false));
            DropColumn("dbo.tbl_outgoing_header", "TOTAL_ITEM");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_outgoing_header", "TOTAL_ITEM", c => c.Int(nullable: false));
            DropColumn("dbo.tbl_outgoing_header", "TOTAL_ITEM_AMOUNT");
        }
    }
}
