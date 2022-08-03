using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [Authorize(Roles = RoleName.CanManageMovies)]
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
            var viewModel = new RentalFormViewModel();

            var moviesList =_context.Movies.ToList();
            var mList = new List<SelectListItem>();
            foreach(var movie in moviesList)
            {
                var selectList = new SelectListItem
                {
                    Text = movie.Name,
                    Value = movie.Id.ToString()
                };
                mList.Add(selectList);
            }

            var customerLists = _context.Customers.ToList();
            var cList = new List<SelectListItem>();
            foreach (var customer in customerLists)
            {
                var selectList = new SelectListItem
                {
                    Text = customer.Name,
                    Value = customer.Id.ToString()
                };
                cList.Add(selectList);
            }
            viewModel.Movies = mList;
            viewModel.Customers = cList;

            return View("RentalForm", viewModel);
        }

        [HttpPost]
        public ViewResult New(RentalFormViewModel newRental)
        {
            if (!ModelState.IsValid)
            {
                return View("RentalForm");
            }
            int SCID = int.Parse(newRental.SelectedCustomerID);
            var customer = _context.Customers.Single(c => c.Id == SCID);

            int SMID = int.Parse(newRental.SelectedMovieID);
            var movie = _context.Movies.Single(c => c.Id == SMID);


            ViewBag.NumberAvail = movie.NumberAvailable;
            ViewBag.Success = false;
            if (movie.NumberAvailable == 0)        // Edge Case => Optimistic
             {
                return View("ConfirmPage");
             }

             movie.NumberAvailable--;  // decreasing 
             ViewBag.Success = true;
             ViewBag.NumberAvail--;

            var rental = new Rental  // rental object
             {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
             };
             _context.Rentals.Add(rental);
             _context.SaveChanges();
            
            return View("ConfirmPage");
        }
    }
}