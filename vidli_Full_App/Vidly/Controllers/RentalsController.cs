using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [AllowAnonymous]
    public class RentalsController : Controller
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        
        public ViewResult New()
        {
            //public List<int> movieIds = _context.Movie();
            var viewModel = new RentalFormViewModel
            {
                CustomerId = 1,
                MovieId = 1
            };

            return View("RentalForm", viewModel);
        }


        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPost]
        public ViewResult New(RentalFormViewModel newRental)
        {
            var customer = _context.Customers.Single(c => c.Id == newRental.CustomerId);
            var movie = _context.Movies.Single(c => c.Id == newRental.MovieId); 

            var viewModel = new RentalFormViewModel
            {
                CustomerId = 1,
                MovieId = 1
            };
             if (movie.NumberAvailable == 0)        // Edge Case => Optimistic
             {
                return View("RentalForm", viewModel);
             }

             movie.NumberAvailable--;  // decreasing 

             var rental = new Rental  // rental object
             {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
             };
             _context.Rentals.Add(rental);
             _context.SaveChanges();
            
            return View("RentalForm", viewModel);
        }
    }
}