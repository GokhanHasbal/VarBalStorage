namespace VarBal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipment3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "Name", c => c.Int(nullable: false));
        }
    }
}
