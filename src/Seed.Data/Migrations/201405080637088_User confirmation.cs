namespace Seed.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Userconfirmation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "HashedPassword", c => c.String(maxLength: 150));
            AddColumn("dbo.Users", "IsConfirmed", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.Users", "LastPasswordChangeUtcDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LastPasswordChangeUtcDate");
            DropColumn("dbo.Users", "IsConfirmed");
            DropColumn("dbo.Users", "HashedPassword");
        }
    }
}
