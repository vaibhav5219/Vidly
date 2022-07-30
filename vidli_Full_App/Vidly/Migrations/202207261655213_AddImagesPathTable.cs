namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagesPathTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImagesPaths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            

        }
        
        public override void Down()
        {
            DropTable("dbo.ImagesPaths");
        }
    }
}
