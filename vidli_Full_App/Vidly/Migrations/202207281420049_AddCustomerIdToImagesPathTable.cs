namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerIdToImagesPathTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImagesPaths", "CustomerId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ImagesPaths", "CustomerId");
        }
    }
}
