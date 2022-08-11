namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerAspNetUsersColumnAspNetUserIdToInt : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CustomerAspNetUsers", new[] { "AspNetUserId" });
            AlterColumn("dbo.CustomerAspNetUsers", "AspNetUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.CustomerAspNetUsers", "AspNetUserId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.CustomerAspNetUsers", new[] { "AspNetUserId" });
            AlterColumn("dbo.CustomerAspNetUsers", "AspNetUserId", c => c.String(nullable: false, maxLength: 450));
            CreateIndex("dbo.CustomerAspNetUsers", "AspNetUserId", unique: true);
        }
    }
}
