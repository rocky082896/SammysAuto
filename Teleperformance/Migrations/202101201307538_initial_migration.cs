namespace Teleperformance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_inbound",
                c => new
                    {
                        inbound_id = c.Int(nullable: false, identity: true),
                        tag_id = c.String(nullable: false, maxLength: 100),
                        date_time = c.String(),
                    })
                .PrimaryKey(t => t.inbound_id);
            
            CreateTable(
                "dbo.tbl_outbound",
                c => new
                    {
                        outbound_id = c.Int(nullable: false, identity: true),
                        tag_id = c.String(nullable: false, maxLength: 100),
                        date_time = c.String(),
                    })
                .PrimaryKey(t => t.outbound_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tbl_outbound");
            DropTable("dbo.tbl_inbound");
        }
    }
}
