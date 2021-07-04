namespace SatoImsV1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_outgoing_details",
                c => new
                    {
                        REC_ID = c.Int(nullable: false, identity: true),
                        RECEIVING_ID = c.String(),
                        OUTGOING_NO = c.String(),
                        ITEM_NO = c.String(),
                        PRICE = c.Double(nullable: false),
                        ITEM_AMOUNT = c.Double(nullable: false),
                        ITEM_QUANTITY = c.Int(nullable: false),
                        CURRENT_QTY = c.Int(nullable: false),
                        ITEM_STATUS = c.Int(nullable: false),
                        CUSTOMER = c.String(),
                        DELIVERYDATE = c.DateTime(),
                    })
                .PrimaryKey(t => t.REC_ID);
            
            CreateTable(
                "dbo.tbl_outgoing_header",
                c => new
                    {
                        REC_ID = c.Int(nullable: false, identity: true),
                        OUTGOING_NO = c.String(),
                        OUTGOING_STATUS = c.String(),
                        TOTAL_ITEM = c.Int(nullable: false),
                        CURRENCY = c.String(maxLength: 5),
                        COMPLETION_DATE = c.DateTime(),
                    })
                .PrimaryKey(t => t.REC_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tbl_outgoing_header");
            DropTable("dbo.tbl_outgoing_details");
        }
    }
}
