namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoviesIdToVideosPathTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VideosPaths", "MovieId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VideosPaths", "MovieId");
        }
    }
}
