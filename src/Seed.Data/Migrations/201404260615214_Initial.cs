namespace Seed.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedByUserId = c.Int(nullable: false),
                        CreatedUtcDate = c.DateTime(nullable: false),
                        ModifiedByUserId = c.Int(nullable: false),
                        ModifiedUtcDate = c.DateTime(nullable: false),
                        Username = c.String(),
                        FullName = c.String(),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
