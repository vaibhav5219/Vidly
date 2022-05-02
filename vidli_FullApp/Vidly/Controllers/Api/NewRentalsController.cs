using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;
        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            var customer = _context.Customers.Single(c => c.Id == newRental.CustomerId);
            var movies = _context.Movies.Where(m => newRental.MovieId.Contains(m.Id)).ToList();  // Select * from movies where Id(1_2_ _ _ n);
           
            
            foreach(var movie in movies)
            {
                if (movie.NumberAvailable == 0)        // Edge Case => Optimistic
                    return BadRequest("Movie is not available. ");

                movie.NumberAvailable--;  // decreasing 

                var rental = new Rental  // rental object
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };
                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
