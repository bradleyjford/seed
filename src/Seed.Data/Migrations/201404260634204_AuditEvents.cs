using System;
using System.Data.Entity.Migrations;

namespace Seed.Data.Migrations
{
    public partial class AuditEvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        Command = c.String(nullable: false, maxLength: 150),
                        Data = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuditEvents");
        }
    }
}
