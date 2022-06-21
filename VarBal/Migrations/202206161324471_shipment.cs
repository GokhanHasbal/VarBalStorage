namespace VarBal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                        LicensePlate = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Shipments", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.Shipments", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Shipments", "VehicleId", c => c.Int(nullable: false));
            AddColumn("dbo.Shipments", "SenderId", c => c.Int(nullable: false));
            AddColumn("dbo.Shipments", "ReceiverId", c => c.Int(nullable: false));
            AddColumn("dbo.Shipments", "Stock", c => c.Int(nullable: false));
            AddColumn("dbo.Shipments", "SendTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Shipments", "ReceiveTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Shipments", "Warehouse_Id", c => c.Int());
            CreateIndex("dbo.Shipments", "ProductId");
            CreateIndex("dbo.Shipments", "UserId");
            CreateIndex("dbo.Shipments", "VehicleId");
            CreateIndex("dbo.Shipments", "Warehouse_Id");
            AddForeignKey("dbo.Shipments", "Warehouse_Id", "dbo.Warehouses", "Id");
            AddForeignKey("dbo.Shipments", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.Shipments", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Shipments", "VehicleId", "dbo.Vehicles", "Id");
            DropColumn("dbo.Shipments", "ShipmentName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shipments", "ShipmentName", c => c.String());
            DropForeignKey("dbo.Shipments", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Shipments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Shipments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Shipments", "Warehouse_Id", "dbo.Warehouses");
            DropIndex("dbo.Shipments", new[] { "Warehouse_Id" });
            DropIndex("dbo.Shipments", new[] { "VehicleId" });
            DropIndex("dbo.Shipments", new[] { "UserId" });
            DropIndex("dbo.Shipments", new[] { "ProductId" });
            DropColumn("dbo.Shipments", "Warehouse_Id");
            DropColumn("dbo.Shipments", "ReceiveTime");
            DropColumn("dbo.Shipments", "SendTime");
            DropColumn("dbo.Shipments", "Stock");
            DropColumn("dbo.Shipments", "ReceiverId");
            DropColumn("dbo.Shipments", "SenderId");
            DropColumn("dbo.Shipments", "VehicleId");
            DropColumn("dbo.Shipments", "UserId");
            DropColumn("dbo.Shipments", "ProductId");
            DropTable("dbo.Vehicles");
        }
    }
}
