namespace Seed.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Concurrency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AlterColumn("dbo.Users", "Username", c => c.String(maxLength: 100));
            AlterColumn("dbo.Users", "FullName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Users", "EmailAddress", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "EmailAddress", c => c.String());
            AlterColumn("dbo.Users", "FullName", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String());
            DropColumn("dbo.Users", "RowVersion");
        }
    }
}
