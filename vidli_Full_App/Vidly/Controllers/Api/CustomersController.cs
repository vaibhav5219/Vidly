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

namespace Vidly.Controllers.Api
{
    [Route("api/Customers/")]
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //HttpGet
        [HttpGet]
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
        public CustomersDto GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return Mapper.Map<Customer, CustomersDto>(customer);
        }

        [HttpPost]
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
        public void UpdateCustomer(int id, CustomersDto customerdto)
        {

            var customerdb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerdb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerdto, customerdb);


            _context.SaveChanges();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var customerdb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerdb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);


            _context.Customers.Remove(customerdb);
            _context.SaveChanges();
        }

        [HttpGet]
        [GET("/api/Customers/Details/{id}")]
        [Route("details")]
        public IHttpActionResult Details(int id)
        {
            if(id <= 0)
            {
                id = 1;
            }
            CustomerDetailsDto customerDetailsDto = new CustomerDetailsDto();

            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            IEnumerable<Rental> rentals = _context.Rentals.Include(m => m.Movie).Include(m => m.Customer).ToList().Where(r => r.Customer.Id == customer.Id);
            IEnumerable<Movie> movies = _context.Movies.Include(m => m.Genre).Where(m => m.Id == id);

            customerDetailsDto.Customer = customer;
            customerDetailsDto.Movies = movies;
            customerDetailsDto.Rentals = rentals;

            return Ok(customerDetailsDto);
        }
    }
}
