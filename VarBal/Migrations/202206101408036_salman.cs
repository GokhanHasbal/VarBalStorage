namespace VarBal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class salman : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "Image");
        }
    }
}
