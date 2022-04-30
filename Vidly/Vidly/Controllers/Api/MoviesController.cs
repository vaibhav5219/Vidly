using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using AutoMapper;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        Context _context;
        public MoviesController() {
            _context = new Context();
        }
        // GET : /api/movies
        [HttpGet]
        public IEnumerable<MovieDto> movies()
        {
            return _context.movies
                .Include(c=>c.Genre)
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
        }
        // GET  : /api/movies/1\
        [HttpGet]
        public MovieDto GetMovie(int id)
        {
            var movie = _context.movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Movie, MovieDto>(movie);
        }
        //  POST : /api/movies
        [HttpPost]
        public MovieDto CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movie = Mapper.Map<MovieDto,Movie>(movieDto);

            _context.movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return movieDto;
        }
        // PUT : /api/movies/1   => Edit the request
        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var moviInDb = _context.movies.SingleOrDefault(m => m.Id == id);

            if (moviInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var movie = Mapper.Map(movieDto, moviInDb);
            // ya ye bhi  => var movie = Mapper.Map<MovieDto, Movie>(movieDto, moviInDb);

            /*moviInDb.Name = movie.Name;
            moviInDb.DateAdded = movie.DateAdded;
            moviInDb.ReleaseDate = movie.ReleaseDate;
            moviInDb.NumberInStocks = movie.NumberInStocks;
            moviInDb.GenreId = movie.GenreId;
            */
            _context.SaveChanges();

        }
        // DELETE : /api/movies/1
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var moviInDb = _context.movies.SingleOrDefault(m => m.Id == id);

            if (moviInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.movies.Remove(moviInDb);
            _context.SaveChanges();
        }

    }
}
