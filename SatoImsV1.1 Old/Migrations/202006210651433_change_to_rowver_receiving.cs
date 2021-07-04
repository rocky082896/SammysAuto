namespace SatoImsV1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_to_rowver_receiving : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_receiving", "date_stored", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_receiving", "date_stored");
        }
    }
}
