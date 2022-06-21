namespace VarBal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vb7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Warehouses", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Warehouses", "Image");
        }
    }
}
