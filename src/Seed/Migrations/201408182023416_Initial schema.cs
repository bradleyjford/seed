namespace Seed.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialschema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Command = c.String(),
                        Data = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedByUserId = c.Guid(nullable: false),
                        CreatedUtcDate = c.DateTime(nullable: false),
                        ModifiedByUserId = c.Guid(nullable: false),
                        ModifiedUtcDate = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedByUserId = c.Guid(nullable: false),
                        CreatedUtcDate = c.DateTime(nullable: false),
                        ModifiedByUserId = c.Guid(nullable: false),
                        ModifiedUtcDate = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        State = c.Int(nullable: false),
                        UserName = c.String(maxLength: 100),
                        HashedPassword = c.String(maxLength: 150),
                        FullName = c.String(maxLength: 100),
                        Email = c.String(maxLength: 150),
                        Notes = c.String(),
                        LastLoginUtcDate = c.DateTime(),
                        LastPasswordChangeUtcDate = c.DateTime(nullable: false),
                        FailedLoginWindowStart = c.DateTime(),
                        FailedLoginAttemptCount = c.Int(nullable: false),
                        LockedUtcDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                        IsPasswordChangeRequired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoginProviders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Name = c.String(nullable: false, maxLength: 200),
                        UserKey = c.String(nullable: false, maxLength: 200),
                        CreatedUtcDate = c.DateTime(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoginProviders", "User_Id", "dbo.Users");
            DropIndex("dbo.LoginProviders", new[] { "User_Id" });
            DropTable("dbo.LoginProviders");
            DropTable("dbo.Users");
            DropTable("dbo.Countries");
            DropTable("dbo.AuditEvents");
        }
    }
}
