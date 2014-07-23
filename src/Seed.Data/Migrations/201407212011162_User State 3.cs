namespace Seed.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserState3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "State", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "State");
        }
    }
}
