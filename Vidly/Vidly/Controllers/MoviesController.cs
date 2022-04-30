using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        private Context _context;
        public MoviesController()
        {
            _context = new Context();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movies/Moview
        
        //[Route("movies")]    // Attribute Route
        public ViewResult Moview()
        { 
            //var movie = _context.movies.Include(m => m.Genre).ToList();
            return View();
        }

        //[Route("movies/{id}")]      
        public ActionResult detail(int id)      // For Details
        {
            var customer = _context.movies.SingleOrDefault(m => m.Id == id);

            if (customer == null)
                return HttpNotFound();
            return View(customer);
        }
        // For Rendering new movie form
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new NewMovieViewModel()
            {
                Movie = new Movie(),
                Genres=genres
            };
            viewModel.Movie.ReleaseDate = DateTime.Now;
            viewModel.Movie.DateAdded = DateTime.Today;

            return View("New",viewModel);
        }
        // For Adding new movie
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewmodel = new NewMovieViewModel
                {
                    Movie = movie,
                    Genres = _context.Genres.ToList()
                };
                return View("New", viewmodel);
            }
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStocks = movie.NumberInStocks;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            _context.SaveChanges();

            return RedirectToAction("Moview", "Movies");
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new NewMovieViewModel()
            {
                Movie = movie,
                Genres = _context.Genres.ToList()

            };

            return View("Edit",viewModel);
        }
        [HttpPost]
        public ActionResult EditForm(Movie movie)
        {
            if (movie.Id == 0)
                return HttpNotFound();

            var movieData = _context.movies.Single( m => m.Id == movie.Id);
            movieData.Name = movie.Name;
            movieData.NumberInStocks = movie.NumberInStocks;
            movieData.ReleaseDate = movie.ReleaseDate;
            movieData.DateAdded = movie.DateAdded;
            movieData.GenreId = movie.GenreId;

            _context.SaveChanges();

            return RedirectToAction("Moview","Movies");
        }
    }
}