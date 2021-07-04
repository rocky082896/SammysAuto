namespace SatoImsV1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_category",
                c => new
                    {
                        cat_id = c.Int(nullable: false, identity: true),
                        cat_desc = c.String(nullable: false, maxLength: 100),
                        date_created = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.cat_id)
                .Index(t => t.cat_desc, unique: true);
            
            CreateTable(
                "dbo.tbl_item_master",
                c => new
                    {
                        item_no = c.String(nullable: false, maxLength: 128),
                        item_desc = c.String(nullable: false),
                        cat_id = c.Int(nullable: false),
                        group_id = c.Int(nullable: false),
                        uom = c.String(nullable: false),
                        currency = c.String(nullable: false, maxLength: 12),
                        price = c.Double(nullable: false),
                        image_src = c.String(maxLength: 200),
                        date_upload = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.item_no)
                .ForeignKey("dbo.tbl_category", t => t.cat_id, cascadeDelete: true)
                .ForeignKey("dbo.tbl_group", t => t.group_id, cascadeDelete: true)
                .Index(t => t.cat_id)
                .Index(t => t.group_id);
            
            CreateTable(
                "dbo.tbl_group",
                c => new
                    {
                        group_id = c.Int(nullable: false, identity: true),
                        group_name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.group_id)
                .Index(t => t.group_name, unique: true);
            
            CreateTable(
                "dbo.tbl_customer",
                c => new
                    {
                        customer_id = c.String(nullable: false, maxLength: 128),
                        customer_name = c.String(maxLength: 65),
                        address = c.String(nullable: false),
                        tin = c.String(maxLength: 15),
                        contact_1 = c.String(nullable: false, maxLength: 14),
                        contact_2 = c.String(maxLength: 14),
                        email = c.String(maxLength: 100),
                        date_created = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        status = c.Int(nullable: false),
                        Invoice_Id = c.Int(),
                        Invoice_invoiceNo = c.String(maxLength: 8),
                    })
                .PrimaryKey(t => t.customer_id)
                .ForeignKey("dbo.tbl_invoice", t => new { t.Invoice_Id, t.Invoice_invoiceNo })
                .Index(t => t.customer_name, unique: true)
                .Index(t => new { t.Invoice_Id, t.Invoice_invoiceNo });
            
            CreateTable(
                "dbo.tbl_dispatch_ledger",
                c => new
                    {
                        ledger_id = c.Int(nullable: false, identity: true),
                        customer_id = c.String(maxLength: 128),
                        item_no = c.String(maxLength: 128),
                        dispatch_qty = c.Int(nullable: false),
                        date_created = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ledger_id)
                .ForeignKey("dbo.tbl_customer", t => t.customer_id)
                .ForeignKey("dbo.tbl_item_master", t => t.item_no)
                .Index(t => t.customer_id)
                .Index(t => t.item_no);
            
            CreateTable(
                "dbo.tbl_invoice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        invoiceNo = c.String(nullable: false, maxLength: 8),
                        poNumber = c.String(nullable: false),
                        customer_id = c.String(maxLength: 128),
                        address = c.String(nullable: false),
                        terms = c.Int(nullable: false),
                        issuanceDate = c.DateTime(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.invoiceNo })
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
                .ForeignKey("dbo.tbl_item_master", t => t.item_no)
                .Index(t => t.item_no);
            
            CreateTable(
                "dbo.tbl_office_po",
                c => new
                    {
                        officePoId = c.Int(nullable: false, identity: true),
                        officePoNumber = c.String(nullable: false, maxLength: 50),
                        supp_id = c.String(maxLength: 11),
                        officeCreditTerms = c.Int(nullable: false),
                        officePoValidity = c.Int(nullable: false),
                        officeRemarks = c.String(maxLength: 100),
                        officeTotalAmount = c.Double(nullable: false),
                        officeDateCreated = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        officeDeliveryDate = c.DateTime(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.officePoId, t.officePoNumber })
                .ForeignKey("dbo.tbl_supplier", t => t.supp_id)
                .Index(t => t.officePoNumber, unique: true)
                .Index(t => t.supp_id);
            
            CreateTable(
                "dbo.tbl_supplier",
                c => new
                    {
                        supp_id = c.String(nullable: false, maxLength: 11),
                        supplier_name = c.String(nullable: false),
                        address = c.String(nullable: false),
                        contact_1 = c.String(nullable: false, maxLength: 32),
                        contact_2 = c.String(),
                        email = c.String(),
                        date_created = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.supp_id);
            
            CreateTable(
                "dbo.tbl_office_po_items",
                c => new
                    {
                        office_item_id = c.Int(nullable: false, identity: true),
                        item_no = c.String(maxLength: 128),
                        officePoNumber = c.String(nullable: false),
                        officeUnit = c.String(nullable: false),
                        officeQuantity = c.Int(nullable: false),
                        officePrice = c.Double(nullable: false),
                        officeAmount = c.Double(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.office_item_id)
                .ForeignKey("dbo.tbl_item_master", t => t.item_no)
                .Index(t => t.item_no);
            
            CreateTable(
                "dbo.tbl_print",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        control_no = c.String(nullable: false, maxLength: 128),
                        item_code = c.String(nullable: false),
                        quantity = c.Int(nullable: false),
                        date_printed = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => new { t.Id, t.control_no });
            
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
                "dbo.tbl_user",
                c => new
                    {
                        user_id = c.String(nullable: false, maxLength: 11),
                        firstname = c.String(nullable: false, maxLength: 35),
                        lastname = c.String(nullable: false, maxLength: 35),
                        password = c.String(nullable: false, maxLength: 25),
                        priv_id = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.user_id)
                .ForeignKey("dbo.tbl_privilege", t => t.priv_id, cascadeDelete: true)
                .Index(t => t.priv_id);
            
            CreateTable(
                "dbo.tbl_receiving",
                c => new
                    {
                        rec_id = c.Int(nullable: false, identity: true),
                        item_no = c.String(maxLength: 128),
                        officePoNumber = c.String(nullable: false),
                        serialNo = c.String(nullable: false),
                        date_rec = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        rec_qty = c.Int(nullable: false),
                        current_qty = c.Int(nullable: false),
                        price = c.Double(nullable: false),
                        user_id = c.String(maxLength: 11),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.rec_id)
                .ForeignKey("dbo.tbl_item_master", t => t.item_no)
                .ForeignKey("dbo.tbl_user", t => t.user_id)
                .Index(t => t.item_no)
                .Index(t => t.user_id);
            
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
                .ForeignKey("dbo.tbl_item_master", t => t.item_no)
                .Index(t => t.item_no);
            
            CreateTable(
                "dbo.tbl_po",
                c => new
                    {
                        poId = c.Int(nullable: false, identity: true),
                        poNumber = c.String(nullable: false, maxLength: 50),
                        creditTerms = c.Int(nullable: false),
                        poValidity = c.Int(nullable: false),
                        remarks = c.String(maxLength: 100),
                        totalAmount = c.Double(nullable: false),
                        dateCreated = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        deliveryDate = c.DateTime(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.poId, t.poNumber })
                .Index(t => t.poNumber, unique: true);
            
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
            DropForeignKey("dbo.tbl_po_items", "item_no", "dbo.tbl_item_master");
            DropForeignKey("dbo.tbl_receiving", "user_id", "dbo.tbl_user");
            DropForeignKey("dbo.tbl_receiving", "item_no", "dbo.tbl_item_master");
            DropForeignKey("dbo.tbl_user", "priv_id", "dbo.tbl_privilege");
            DropForeignKey("dbo.tbl_office_po_items", "item_no", "dbo.tbl_item_master");
            DropForeignKey("dbo.tbl_office_po", "supp_id", "dbo.tbl_supplier");
            DropForeignKey("dbo.tbl_item_status", "item_no", "dbo.tbl_item_master");
            DropForeignKey("dbo.tbl_customer", new[] { "Invoice_Id", "Invoice_invoiceNo" }, "dbo.tbl_invoice");
            DropForeignKey("dbo.tbl_invoice", "customer_id", "dbo.tbl_customer");
            DropForeignKey("dbo.tbl_dispatch_ledger", "item_no", "dbo.tbl_item_master");
            DropForeignKey("dbo.tbl_dispatch_ledger", "customer_id", "dbo.tbl_customer");
            DropForeignKey("dbo.tbl_item_master", "group_id", "dbo.tbl_group");
            DropForeignKey("dbo.tbl_item_master", "cat_id", "dbo.tbl_category");
            DropIndex("dbo.tbl_po", new[] { "poNumber" });
            DropIndex("dbo.tbl_po_items", new[] { "item_no" });
            DropIndex("dbo.tbl_receiving", new[] { "user_id" });
            DropIndex("dbo.tbl_receiving", new[] { "item_no" });
            DropIndex("dbo.tbl_user", new[] { "priv_id" });
            DropIndex("dbo.tbl_office_po_items", new[] { "item_no" });
            DropIndex("dbo.tbl_office_po", new[] { "supp_id" });
            DropIndex("dbo.tbl_office_po", new[] { "officePoNumber" });
            DropIndex("dbo.tbl_item_status", new[] { "item_no" });
            DropIndex("dbo.tbl_invoice", new[] { "customer_id" });
            DropIndex("dbo.tbl_dispatch_ledger", new[] { "item_no" });
            DropIndex("dbo.tbl_dispatch_ledger", new[] { "customer_id" });
            DropIndex("dbo.tbl_customer", new[] { "Invoice_Id", "Invoice_invoiceNo" });
            DropIndex("dbo.tbl_customer", new[] { "customer_name" });
            DropIndex("dbo.tbl_group", new[] { "group_name" });
            DropIndex("dbo.tbl_item_master", new[] { "group_id" });
            DropIndex("dbo.tbl_item_master", new[] { "cat_id" });
            DropIndex("dbo.tbl_category", new[] { "cat_desc" });
            DropTable("dbo.tbl_status");
            DropTable("dbo.tbl_po");
            DropTable("dbo.tbl_po_items");
            DropTable("dbo.tbl_receiving");
            DropTable("dbo.tbl_user");
            DropTable("dbo.tbl_privilege");
            DropTable("dbo.tbl_print");
            DropTable("dbo.tbl_office_po_items");
            DropTable("dbo.tbl_supplier");
            DropTable("dbo.tbl_office_po");
            DropTable("dbo.tbl_item_status");
            DropTable("dbo.tbl_invoice");
            DropTable("dbo.tbl_dispatch_ledger");
            DropTable("dbo.tbl_customer");
            DropTable("dbo.tbl_group");
            DropTable("dbo.tbl_item_master");
            DropTable("dbo.tbl_category");
        }
    }
}
