namespace Seed.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Userstate4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsPasswordChangeRequired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsPasswordChangeRequired");
            DropColumn("dbo.Users", "IsConfirmed");
            DropColumn("dbo.Users", "IsActive");
        }
    }
}
