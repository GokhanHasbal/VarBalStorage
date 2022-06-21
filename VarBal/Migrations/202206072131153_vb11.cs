namespace VarBal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vb11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AboutFeatures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AboutFeatures");
        }
    }
}
