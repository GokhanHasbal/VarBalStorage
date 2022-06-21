namespace VarBal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipship : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shipments", "SendTime", c => c.DateTime());
            AlterColumn("dbo.Shipments", "ReceiveTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shipments", "ReceiveTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Shipments", "SendTime", c => c.DateTime(nullable: false));
        }
    }
}
