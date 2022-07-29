namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieIdToImagesPathTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImagesPaths", "MovieId", c => c.Int(nullable: false));
            DropColumn("dbo.ImagesPaths", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ImagesPaths", "CustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.ImagesPaths", "MovieId");
        }
    }
}
