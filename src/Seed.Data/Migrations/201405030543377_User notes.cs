namespace Seed.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Usernotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Notes");
        }
    }
}
