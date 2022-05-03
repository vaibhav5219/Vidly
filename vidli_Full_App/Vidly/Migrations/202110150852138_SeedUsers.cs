namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'580cad87-d9e8-4518-8606-3e42b51f3756', N'guest@vidly.com', 0, N'AG/EPR44B2raPxUBMZpgAgSKW3F/De66knR/5B5KVZwtVRrjUgBW9n4VovTO7boOdA==', N'18af4c07-c46e-4111-ab3e-422eb9a4e09d', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5e985fd8-2599-4236-b960-468845024068', N'admin@vidly.com', 0, N'AG7b/C3xdSgFHp5zGHJLU1pybeuMPUAchX33HFa7RiMYo6cltHuvtCa3KSUxhBqWEw==', N'9a634653-f533-4cbe-abf1-43da67329e5f', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'78c7ec86-4da8-48ab-9402-990dfeb64576', N'canManageMovies')

            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5e985fd8-2599-4236-b960-468845024068', N'78c7ec86-4da8-48ab-9402-990dfeb64576')
            
");
        }
        
        public override void Down()
        {
        }
    }
}
