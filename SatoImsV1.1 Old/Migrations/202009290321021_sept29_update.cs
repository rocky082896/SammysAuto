namespace SatoImsV1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sept29_update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_outgoing",
                c => new
                    {
                        outgoing_id = c.Int(nullable: false, identity: true),
                        outgoing_no = c.String(),
                        Status = c.Int(nullable: false),
                        total_quantity = c.Int(nullable: false),
                        currency = c.String(maxLength: 5),
                        date_completion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.outgoing_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tbl_outgoing");
        }
    }
}
