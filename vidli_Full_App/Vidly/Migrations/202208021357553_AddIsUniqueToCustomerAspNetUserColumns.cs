namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsUniqueToCustomerAspNetUserColumns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerAspNetUsers", "AspNetUserId", c => c.String(nullable: false, maxLength: 450));
            CreateIndex("dbo.CustomerAspNetUsers", "AspNetUserId", unique: true);
            CreateIndex("dbo.CustomerAspNetUsers", "CustomerId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.CustomerAspNetUsers", new[] { "CustomerId" });
            DropIndex("dbo.CustomerAspNetUsers", new[] { "AspNetUserId" });
            AlterColumn("dbo.CustomerAspNetUsers", "AspNetUserId", c => c.String());
        }
    }
}
