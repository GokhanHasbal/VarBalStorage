namespace VarBal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class booking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameSurname = c.String(),
                        Email = c.String(),
                        MyProperty = c.Int(nullable: false),
                        PostCode = c.String(),
                        WarehouseId = c.Int(nullable: false),
                        Note = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        View = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseId)
                .Index(t => t.WarehouseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "WarehouseId", "dbo.Warehouses");
            DropIndex("dbo.Bookings", new[] { "WarehouseId" });
            DropTable("dbo.Bookings");
        }
    }
}
