namespace Seed.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Externalloginproviders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoginProviders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        UserKey = c.String(nullable: false, maxLength: 200),
                        CreatedUtcDate = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoginProviders", "User_Id", "dbo.Users");
            DropIndex("dbo.LoginProviders", new[] { "User_Id" });
            DropTable("dbo.LoginProviders");
        }
    }
}
