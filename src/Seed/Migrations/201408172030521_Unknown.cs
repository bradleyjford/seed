namespace Seed.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Unknown : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Countries", "CreatedUtcDate", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
            AlterColumn("dbo.Countries", "ModifiedUtcDate", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
            AlterColumn("dbo.Users", "CreatedUtcDate", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
            AlterColumn("dbo.Users", "ModifiedUtcDate", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
            AlterColumn("dbo.LoginProviders", "CreatedUtcDate", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LoginProviders", "CreatedUtcDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "ModifiedUtcDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "CreatedUtcDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Countries", "ModifiedUtcDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Countries", "CreatedUtcDate", c => c.DateTime(nullable: false));
        }
    }
}
