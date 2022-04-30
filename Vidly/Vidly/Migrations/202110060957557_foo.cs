namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foo : DbMigration
    {
        public override void Up() { 
      
            Sql("Insert Into MovieS(Name, ReleaseDate, NumberInStocks, DateAdded, GenreId) VALUES ('Raone',2020-09-09,55,2020-09-10,2)");
        }
        
        public override void Down()
        {
        }
    }
}
