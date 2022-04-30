using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private Context _context;
        public CustomersController()
        {
            _context = new Context();  // This dbcontext is a disposable object
        }
        protected override void Dispose(bool disposing)  // Overriding Dispose method
        {
           _context.Dispose();  
        }
        // GET: Customers/index
        public ViewResult Index()
        {
            // Deffered execution when not calling ToList() method
            //   var customers = _context.Customers.Include(c => c.MembershipType).ToList();// when we iterate over customers then query will generate
            // Include method for Eager Loading/ Nevagating the Membeship table
            //   return View(customers);
            return View();
        }
        //  customer/detail/id
        public ActionResult Detail(int id)
        {
            var c = _context.Customers.SingleOrDefault(cu => cu.MembershipTypeId == id);

            if (c == null)
                return HttpNotFound();
            return View(c);
        }
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new NewCustomerViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            // For Validating
            if (!ModelState.IsValid)
            {
                var viewModel = new NewCustomerViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
               
                return View("New", viewModel);
            }

            // For Updating
            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {  
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                // 1st WAY
                        //TryUpdateModel(customerInDb);
                //  2ns Way
                        //TryUpdateModel(customerInDb,"",new String("Name","Email"));
                // mAPPER
                        //  Mapper.Map(customer, customerInDb);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.IsSuscribedToNewsLetter = customer.IsSuscribedToNewsLetter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;

            }
            _context.SaveChanges();

            return View("Index", "Customers");
        }
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new NewCustomerViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm",viewModel);  // viewModel ke object ko form me pass kiya gya hai esliye map ho gya hai 
        }
      //  private IEnumerable<Customer> GetCustomers()
      //  {
      //      return new List<Customer>
      //      {
      //          new Customer {Id=1, Name= "John Smith"},
      //          new Customer { Id=2, Name="Mary Williams"}
      //      };
      //  }
    }
}