namespace VarBal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stıorage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Warehouses", "Email", c => c.String());
            AddColumn("dbo.Warehouses", "Phone", c => c.String());
            AddColumn("dbo.Warehouses", "OfficeHours", c => c.String());
            AddColumn("dbo.Warehouses", "AccessHours", c => c.String());
            AddColumn("dbo.Warehouses", "GoogleMaps", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Warehouses", "GoogleMaps");
            DropColumn("dbo.Warehouses", "AccessHours");
            DropColumn("dbo.Warehouses", "OfficeHours");
            DropColumn("dbo.Warehouses", "Phone");
            DropColumn("dbo.Warehouses", "Email");
        }
    }
}
