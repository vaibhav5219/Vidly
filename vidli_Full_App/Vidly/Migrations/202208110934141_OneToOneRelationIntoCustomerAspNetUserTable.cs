namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToOneRelationIntoCustomerAspNetUserTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CustomerAspNetUsers", new[] { "AspNetUserId" });
            AddColumn("dbo.CustomerAspNetUsers", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.CustomerAspNetUsers", "ApplicationUserId", unique: true);
            AddForeignKey("dbo.CustomerAspNetUsers", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CustomerAspNetUsers", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            DropColumn("dbo.CustomerAspNetUsers", "AspNetUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerAspNetUsers", "AspNetUserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CustomerAspNetUsers", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerAspNetUsers", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.CustomerAspNetUsers", new[] { "ApplicationUserId" });
            DropColumn("dbo.CustomerAspNetUsers", "ApplicationUserId");
            CreateIndex("dbo.CustomerAspNetUsers", "AspNetUserId", unique: true);
        }
    }
}
