namespace VarBal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vb1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        State = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        Price = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WarehouseId = c.Int(),
                        UserId = c.Int(),
                        Title = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        District = c.String(),
                        Street = c.String(),
                        PostCode = c.String(),
                        BuildingNumber = c.String(),
                        FullAddress = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseId)
                .Index(t => t.WarehouseId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNo = c.String(),
                        PoductId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        AdressId = c.Int(nullable: false),
                        CargoId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        View = c.Boolean(nullable: false),
                        PermissionId = c.Int(nullable: false),
                        Address_Id = c.Int(),
                        Product_Id = c.Int(),
                        Shipment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .ForeignKey("dbo.Permissions", t => t.PermissionId)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.Shipments", t => t.Shipment_Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.PermissionId)
                .Index(t => t.Address_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.Shipment_Id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        InvoiceNo = c.Int(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        View = c.Boolean(nullable: false),
                        PermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permissions", t => t.PermissionId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Action = c.String(),
                        Page = c.String(),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        RegisterDate = c.DateTime(nullable: false),
                        Barcode = c.String(),
                        Price = c.Double(nullable: false),
                        Stock = c.Int(nullable: false),
                        Tax = c.Double(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                        ShelfId = c.Int(nullable: false),
                        WarehouseId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        View = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Shelves", t => t.ShelfId)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseId)
                .Index(t => t.CategoryId)
                .Index(t => t.BrandId)
                .Index(t => t.ShelfId)
                .Index(t => t.WarehouseId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        Description = c.String(),
                        MainCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shelves",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        MainShelfId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShipmentName = c.String(),
                        Description = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        View = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adress1 = c.String(),
                        Adress2 = c.String(),
                        Phone1 = c.String(),
                        Phone2 = c.String(),
                        Mail1 = c.String(),
                        Mail2 = c.String(),
                        Description = c.String(),
                        Facebook = c.String(),
                        Twitter = c.String(),
                        Instagram = c.String(),
                        Youtube = c.String(),
                        Linkedin = c.String(),
                        Pinterest = c.String(),
                        GoogleMaps = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewsGets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mail = c.String(),
                        Date = c.DateTime(nullable: false),
                        View = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sliders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        Link = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "Shipment_Id", "dbo.Shipments");
            DropForeignKey("dbo.Orders", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "WarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.Addresses", "WarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.Products", "ShelfId", "dbo.Shelves");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Orders", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.Users", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.Logs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Addresses", "UserId", "dbo.Users");
            DropForeignKey("dbo.Invoices", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Products", new[] { "WarehouseId" });
            DropIndex("dbo.Products", new[] { "ShelfId" });
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Logs", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "PermissionId" });
            DropIndex("dbo.Invoices", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "Shipment_Id" });
            DropIndex("dbo.Orders", new[] { "Product_Id" });
            DropIndex("dbo.Orders", new[] { "Address_Id" });
            DropIndex("dbo.Orders", new[] { "PermissionId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Addresses", new[] { "UserId" });
            DropIndex("dbo.Addresses", new[] { "WarehouseId" });
            DropTable("dbo.Sliders");
            DropTable("dbo.NewsGets");
            DropTable("dbo.Contacts");
            DropTable("dbo.Shipments");
            DropTable("dbo.Warehouses");
            DropTable("dbo.Shelves");
            DropTable("dbo.Categories");
            DropTable("dbo.Brands");
            DropTable("dbo.Products");
            DropTable("dbo.Logs");
            DropTable("dbo.Users");
            DropTable("dbo.Permissions");
            DropTable("dbo.Invoices");
            DropTable("dbo.Orders");
            DropTable("dbo.Addresses");
            DropTable("dbo.Abouts");
        }
    }
}
