﻿
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Vidly.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public object Movie { get; internal set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<ImagesPath> ImagesPaths { get; set; }
        public DbSet<VideosPath> videosPaths { get; set; }
        public DbSet<CustomerAspNetUser> customerAspNetUsers { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}