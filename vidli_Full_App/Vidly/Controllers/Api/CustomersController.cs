using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http;
using Vidly.App_Start;
using Vidly.Models;
using Vidly.Dtos;
using AttributeRouting.Web.Http;
using Microsoft.AspNet.Identity;

namespace Vidly.Controllers.Api
{
    [RoutePrefix("api/Customers")]
    [AllowAnonymous]
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //HttpGet
        [HttpGet]
        [Authorize(Roles = "canManageMovies")]
        public IHttpActionResult GetAllCustomer(string query = null)  //IEnumerable<CustomersDto>
        {
            var customerQuery = _context.Customers
                .Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
                customerQuery = customerQuery.Where(c => c.Name.Contains(query));

            var customerDtos = customerQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomersDto>);

            return Ok(customerDtos);
        }

        [HttpGet]
        [Authorize(Roles = "canManageMovies")]
        public CustomersDto GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return Mapper.Map<Customer, CustomersDto>(customer);
        }

        [HttpPost]
        [Authorize(Roles = "canManageMovies")]
        public CustomersDto CreateCustomer(CustomersDto customerdto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customer = Mapper.Map<CustomersDto, Customer>(customerdto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customerdto;
        }

        [HttpPut]
        [Authorize(Roles = "canManageMovies")]
        public void UpdateCustomer(int id, CustomersDto customerdto)
        {

            var customerdb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerdb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerdto, customerdb);


            _context.SaveChanges();
        }

        [HttpDelete]
        [Authorize(Roles = "canManageMovies")]
        [Authorize(Roles = "isAcustomer")]
        public void Delete(int id)
        {
            var customerdb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerdb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);


            _context.Customers.Remove(customerdb);
            _context.SaveChanges();
        }

        [HttpGet]
        [Route("Details/{id}")]
        [Authorize(Roles ="isAcustomer")]
        public IHttpActionResult Details(int? id)
        {
            if (!(User.IsInRole("isAcustomer")))
            {
                return BadRequest();
            }
            id = 1; 
            string userId = User.Identity.GetUserId();

            //CustomerDetailsDto customerDetailsDto = new CustomerDetailsDto();
            CustomerAspNetUser customerAspNetUser = _context.customerAspNetUsers.FirstOrDefault(c=>c.ApplicationUserId == userId);

            var customer = _context.Customers.SingleOrDefault(c => c.Id == customerAspNetUser.CustomerId);
            IEnumerable<Rental> rentals = _context.Rentals.Include(m => m.Movie).Include(m => m.Customer).ToList().Where(r => r.Customer.Id == customer.Id);
            //IEnumerable<Movie> movies = _context.Movies.Include(m => m.Genre).Where(m => m.Id == id);

            //customerDetailsDto.Customer = customer;
            //customerDetailsDto.Movies = movies;
            //customerDetailsDto.Rentals = rentals;

            return Ok(rentals);
        }
    }
}
