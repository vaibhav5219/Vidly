namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomer : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Customers(Name,IsSubscribedToNewsletter,MembershipTypeId,Birthdate)VALUES('Shalu',1,2,09-09-1999)");
            Sql("INSERT INTO Customers(Name,IsSubscribedToNewsletter,MembershipTypeId,Birthdate)VALUES('banti',0,1,08-09-1999)");
            Sql("INSERT INTO Customers(Name,IsSubscribedToNewsletter,MembershipTypeId,Birthdate)VALUES('Deepika',1,2,09-09-1999)");
           
        }
        
        public override void Down()
        {
        }
    }
}
