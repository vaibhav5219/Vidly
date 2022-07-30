using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDetailsDto
    {
        public Customer Customer { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Rental> Rentals { get; set; }
    }
}