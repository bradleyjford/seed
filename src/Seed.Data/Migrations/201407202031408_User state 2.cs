namespace Seed.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Userstate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "LockedUtcDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "LockedUtcDate", c => c.DateTime(nullable: false));
        }
    }
}
