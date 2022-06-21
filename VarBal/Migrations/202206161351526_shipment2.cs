namespace VarBal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipment2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Vehicles", "Delete", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Shipments", "Status", c => c.String());
            AlterColumn("dbo.Shipments", "SenderId", c => c.Int());
            AlterColumn("dbo.Shipments", "ReceiverId", c => c.Int());
            AlterColumn("dbo.Shipments", "Stock", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shipments", "Stock", c => c.Int(nullable: false));
            AlterColumn("dbo.Shipments", "ReceiverId", c => c.Int(nullable: false));
            AlterColumn("dbo.Shipments", "SenderId", c => c.Int(nullable: false));
            AlterColumn("dbo.Shipments", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.Vehicles", "Delete");
            DropColumn("dbo.Vehicles", "Status");
        }
    }
}
