namespace VarBal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class booking2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "Size", c => c.String());
            AddColumn("dbo.Bookings", "BookTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Bookings", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "MyProperty", c => c.Int(nullable: false));
            DropColumn("dbo.Bookings", "BookTime");
            DropColumn("dbo.Bookings", "Size");
        }
    }
}
