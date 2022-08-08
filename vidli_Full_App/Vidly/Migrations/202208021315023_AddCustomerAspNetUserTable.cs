namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerAspNetUserTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerAspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AspNetUserId = c.String(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            //DropColumn("dbo.Customers", "AspNetUsersLoginId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "AspNetUsersLoginId", c => c.String());
            DropTable("dbo.CustomerAspNetUsers");
        }
    }
}
