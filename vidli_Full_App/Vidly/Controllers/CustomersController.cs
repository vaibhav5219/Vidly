using System.Data.Entity;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Collections.Generic;
using System.Dynamic;

namespace Vidly.Controllers
{
    [AllowAnonymous]
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
        
        public ViewResult Index()
        {
            // Data caching
          /*  if (MemoryCache.Default["genre"] == null)
            {
                MemoryCache.Default["genre"] = _context.Genres.ToList();
            }
            else
            {
                var genre = MemoryCache.Default["genres"] as IEnumerable<Genre>;
            }  */
            // var customers = _context.Customers.Include(c => c.MembershipType).ToList();

             return View();
        }

        public ActionResult Details(int id)
        {// Developement in process
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            IEnumerable<Rental> rentals = _context.Rentals.Include(m => m.Movie).Include(m => m.Customer).ToList().Where(r => r.Customer.Id == customer.Id);
            IEnumerable<Movie> movies = _context.Movies.Include(m => m.Genre).Where(m => m.Id == id);

            //var viewModel = new CustomerDetailFormViewModel
            //{
            //    Customer = customer,
            //    Movies = movies,
            //    Rentals = rentals
            //};
            if (customer == null)
                return HttpNotFound();

            dynamic model = new ExpandoObject();
            model.customer = customer;
            model.movies = movies;
            model.rentals = rentals;

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}