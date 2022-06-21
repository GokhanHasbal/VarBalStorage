namespace VarBal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class son1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shipments", "SendTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shipments", "SendTime", c => c.DateTime());
        }
    }
}
