namespace VarBal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vb14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Code");
        }
    }
}
