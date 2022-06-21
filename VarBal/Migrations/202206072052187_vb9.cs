namespace VarBal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vb9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OurWorkers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        NameSurname = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OurWorkers");
        }
    }
}
