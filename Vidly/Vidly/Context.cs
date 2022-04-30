using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Vidly.Models;

namespace Vidly
{
    public class Context:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public object Movie { get; internal set; }
    }
} 