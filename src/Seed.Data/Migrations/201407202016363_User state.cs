namespace Seed.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Userstate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "LastLoginUtcDate", c => c.DateTime());
            AddColumn("dbo.Users", "FailedLoginWindowStart", c => c.DateTime());
            AddColumn("dbo.Users", "FailedLoginAttemptCount", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "LockedUtcDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.LoginProviders", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Users", "IsActive");
            DropColumn("dbo.Users", "IsConfirmed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "IsConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.LoginProviders", "Discriminator");
            DropColumn("dbo.Users", "LockedUtcDate");
            DropColumn("dbo.Users", "FailedLoginAttemptCount");
            DropColumn("dbo.Users", "FailedLoginWindowStart");
            DropColumn("dbo.Users", "LastLoginUtcDate");
        }
    }
}
