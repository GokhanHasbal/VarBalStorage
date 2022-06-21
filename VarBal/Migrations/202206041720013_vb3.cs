namespace VarBal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vb3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "Selection", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "Image", c => c.String());
            AddColumn("dbo.Shelves", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shelves", "Image");
            DropColumn("dbo.Users", "Image");
            DropColumn("dbo.Addresses", "Selection");
        }
    }
}
