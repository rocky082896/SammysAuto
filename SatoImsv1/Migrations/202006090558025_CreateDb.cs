namespace SatoImsv1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_category",
                c => new
                    {
                        cat_id = c.Int(nullable: false, identity: true),
                        cat_desc = c.String(nullable: false, maxLength: 35),
                        date_created = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.cat_id);
            
            CreateTable(
                "dbo.tbl_customer",
                c => new
                    {
                        customer_id = c.String(nullable: false, maxLength: 128),
                        customer_name = c.String(maxLength: 65),
                        address = c.String(nullable: false, maxLength: 65),
                        tin = c.String(maxLength: 15),
                        contact_1 = c.String(nullable: false, maxLength: 14),
                        contact_2 = c.String(maxLength: 14),
                        email = c.String(maxLength: 35),
                        date_created = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.customer_id)
                .Index(t => t.customer_name, unique: true);
            
            CreateTable(
                "dbo.tbl_dispatch_ledger",
                c => new
                    {
                        ledger_id = c.Int(nullable: false, identity: true),
                        item_no = c.String(maxLength: 128),
                        customer_id = c.String(maxLength: 128),
                        dispatch_qty = c.Int(nullable: false),
                        date_created = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ledger_id)
                .ForeignKey("dbo.tbl_customer", t => t.customer_id)
                .ForeignKey("dbo.item_no", t => t.item_no)
                .Index(t => t.item_no)
                .Index(t => t.customer_id);
            
            CreateTable(
                "dbo.item_no",
                c => new
                    {
                        item_no = c.String(nullable: false, maxLength: 128),
                        item_desc = c.String(nullable: false, maxLength: 65),
                        cat_id = c.Int(nullable: false),
                        uom = c.String(nullable: false),
                        currency = c.String(nullable: false, maxLength: 5),
                        price = c.Double(nullable: false),
                        image_src = c.String(maxLength: 100),
                        date_upload = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.item_no)
                .ForeignKey("dbo.tbl_category", t => t.cat_id, cascadeDelete: true)
                .Index(t => t.cat_id);
            
            CreateTable(
                "dbo.tbl_invoice",
                c => new
                    {
                        invoiceNo = c.String(nullable: false, maxLength: 8),
                        poNumber = c.String(nullable: false),
                        customer_id = c.String(maxLength: 128),
                        address = c.String(nullable: false),
                        terms = c.Int(nullable: false),
                        issuanceDate = c.DateTime(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.invoiceNo)
                .ForeignKey("dbo.tbl_customer", t => t.customer_id)
                .Index(t => t.customer_id);
            
            CreateTable(
                "dbo.tbl_item_status",
                c => new
                    {
                        item_stat_id = c.Int(nullable: false, identity: true),
                        item_no = c.String(maxLength: 128),
                        good_qty = c.Int(nullable: false),
                        bad_qty = c.Int(nullable: false),
                        date_updated = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.item_stat_id)
                .ForeignKey("dbo.item_no", t => t.item_no)
                .Index(t => t.item_no);
            
            CreateTable(
                "dbo.tbl_print",
                c => new
                    {
                        control_no = c.String(nullable: false, maxLength: 128),
                        item_code = c.String(nullable: false, maxLength: 25),
                        quantity = c.Int(nullable: false),
                        date_printed = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.control_no);
            
            CreateTable(
                "dbo.tbl_privilege",
                c => new
                    {
                        priv_id = c.Int(nullable: false, identity: true),
                        admin_rights = c.Int(nullable: false),
                        user_rights = c.Int(nullable: false),
                        receiving_rights = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.priv_id);
            
            CreateTable(
                "dbo.tbl_po_items",
                c => new
                    {
                        item_id = c.Int(nullable: false, identity: true),
                        item_no = c.String(maxLength: 128),
                        poNumber = c.String(nullable: false),
                        unit = c.String(nullable: false),
                        quantity = c.Int(nullable: false),
                        price = c.Double(nullable: false),
                        amount = c.Double(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.item_id)
                .ForeignKey("dbo.item_no", t => t.item_no)
                .Index(t => t.item_no);
            
            CreateTable(
                "dbo.tbl_po",
                c => new
                    {
                        poId = c.Int(nullable: false, identity: true),
                        poNumber = c.String(nullable: false, maxLength: 128),
                        creditTerms = c.Int(nullable: false),
                        poValidity = c.Int(nullable: false),
                        remarks = c.String(maxLength: 50),
                        totalAmount = c.Double(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.poId, t.poNumber });
            
            CreateTable(
                "dbo.tbl_receiving",
                c => new
                    {
                        rec_id = c.Int(nullable: false, identity: true),
                        item_no = c.String(maxLength: 128),
                        serialNo = c.String(nullable: false),
                        supp_id = c.String(maxLength: 128),
                        date_rec = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        rec_qty = c.Int(nullable: false),
                        current_qty = c.Int(nullable: false),
                        user_id = c.String(maxLength: 11),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.rec_id)
                .ForeignKey("dbo.item_no", t => t.item_no)
                .ForeignKey("dbo.tbl_supplier", t => t.supp_id)
                .ForeignKey("dbo.tbl_user", t => t.user_id)
                .Index(t => t.item_no)
                .Index(t => t.supp_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.tbl_supplier",
                c => new
                    {
                        supp_id = c.String(nullable: false, maxLength: 128),
                        supplier_name = c.String(nullable: false, maxLength: 65),
                        address = c.String(nullable: false, maxLength: 65),
                        contact_1 = c.String(nullable: false, maxLength: 15),
                        contact_2 = c.String(),
                        email = c.String(),
                        date_created = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.supp_id);
            
            CreateTable(
                "dbo.tbl_user",
                c => new
                    {
                        user_id = c.String(nullable: false, maxLength: 11),
                        firstname = c.String(nullable: false, maxLength: 25),
                        lastname = c.String(nullable: false, maxLength: 25),
                        password = c.String(nullable: false, maxLength: 10),
                        priv_id = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.user_id)
                .ForeignKey("dbo.tbl_privilege", t => t.priv_id, cascadeDelete: true)
                .Index(t => t.priv_id);
            
            CreateTable(
                "dbo.tbl_status",
                c => new
                    {
                        status = c.Int(nullable: false, identity: true),
                        status_desc = c.String(nullable: false, maxLength: 12),
                    })
                .PrimaryKey(t => t.status);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_receiving", "user_id", "dbo.tbl_user");
            DropForeignKey("dbo.tbl_user", "priv_id", "dbo.tbl_privilege");
            DropForeignKey("dbo.tbl_receiving", "supp_id", "dbo.tbl_supplier");
            DropForeignKey("dbo.tbl_receiving", "item_no", "dbo.item_no");
            DropForeignKey("dbo.tbl_po_items", "item_no", "dbo.item_no");
            DropForeignKey("dbo.tbl_item_status", "item_no", "dbo.item_no");
            DropForeignKey("dbo.tbl_invoice", "customer_id", "dbo.tbl_customer");
            DropForeignKey("dbo.tbl_dispatch_ledger", "item_no", "dbo.item_no");
            DropForeignKey("dbo.item_no", "cat_id", "dbo.tbl_category");
            DropForeignKey("dbo.tbl_dispatch_ledger", "customer_id", "dbo.tbl_customer");
            DropIndex("dbo.tbl_user", new[] { "priv_id" });
            DropIndex("dbo.tbl_receiving", new[] { "user_id" });
            DropIndex("dbo.tbl_receiving", new[] { "supp_id" });
            DropIndex("dbo.tbl_receiving", new[] { "item_no" });
            DropIndex("dbo.tbl_po_items", new[] { "item_no" });
            DropIndex("dbo.tbl_item_status", new[] { "item_no" });
            DropIndex("dbo.tbl_invoice", new[] { "customer_id" });
            DropIndex("dbo.item_no", new[] { "cat_id" });
            DropIndex("dbo.tbl_dispatch_ledger", new[] { "customer_id" });
            DropIndex("dbo.tbl_dispatch_ledger", new[] { "item_no" });
            DropIndex("dbo.tbl_customer", new[] { "customer_name" });
            DropTable("dbo.tbl_status");
            DropTable("dbo.tbl_user");
            DropTable("dbo.tbl_supplier");
            DropTable("dbo.tbl_receiving");
            DropTable("dbo.tbl_po");
            DropTable("dbo.tbl_po_items");
            DropTable("dbo.tbl_privilege");
            DropTable("dbo.tbl_print");
            DropTable("dbo.tbl_item_status");
            DropTable("dbo.tbl_invoice");
            DropTable("dbo.item_no");
            DropTable("dbo.tbl_dispatch_ledger");
            DropTable("dbo.tbl_customer");
            DropTable("dbo.tbl_category");
        }
    }
}
