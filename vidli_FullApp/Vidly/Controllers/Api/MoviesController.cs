using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //HttpGet
        [HttpGet]
        public IEnumerable<MoviesDto> GetAllMovie(string query=null)
        {
            var moviesQuery = _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!string.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));
            
            return moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MoviesDto>);
        }

        [HttpGet]
        public MoviesDto GetCustomer(int id)
        {

            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return Mapper.Map<Movie, MoviesDto>(movie);

        }
        [HttpPost]
        public MoviesDto CreateCustomer(MoviesDto moviedto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var movie = Mapper.Map<MoviesDto, Movie>(moviedto);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return moviedto;
        }

        [HttpPut]
        public void UpdateCustomer(int id, MoviesDto moviedto)
        {

            var moviedb = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (moviedb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(moviedto, moviedb);

            _context.SaveChanges();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var moviedb = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (moviedb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(moviedb);
            _context.SaveChanges();

        }

    }
}
