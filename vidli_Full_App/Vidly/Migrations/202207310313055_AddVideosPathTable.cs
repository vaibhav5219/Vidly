namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVideosPathTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VideosPaths",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        VideoPath = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VideosPaths");
        }
    }
}
