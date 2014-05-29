namespace Seed.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Somethingchanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuditEvents", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuditEvents", "RowVersion");
        }
    }
}
