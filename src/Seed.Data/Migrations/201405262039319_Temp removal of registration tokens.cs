namespace Seed.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tempremovalofregistrationtokens : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "ResetPasswordToken_Secret");
            DropColumn("dbo.Users", "ResetPasswordToken_ExpiryUtcDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ResetPasswordToken_ExpiryUtcDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "ResetPasswordToken_Secret", c => c.String(maxLength: 100));
        }
    }
}
