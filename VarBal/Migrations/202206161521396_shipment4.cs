namespace VarBal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipment4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipments", "Process", c => c.String());
            AlterColumn("dbo.Shipments", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shipments", "Status", c => c.String());
            DropColumn("dbo.Shipments", "Process");
        }
    }
}
