using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Migrations;
using Vidly.Models;
using Vidly.ViewModels;
using System.IO;
using System.Web;
using Microsoft.AspNet.Identity;
using Vidly.Models;
using System.Dynamic;

namespace Vidly.Controllers
{
    [AllowAnonymous]
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            //  var movies = _context.Movies.Include(m => m.Genre).ToList();
            //  return View(movies);

            if (User.IsInRole("canManageMovies"))
            {
                return View("List");
            }
            return View("ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ViewResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            { 
                Movie = new Movie(),
                Genres = genres
            };

            viewModel.Movie.ReleaseDate = DateTime.Now;

            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }


        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);

        }


        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(MovieFormViewModel movieFormViewModel)
        {
            if (!ModelState.IsValid) 
            {
                var viewmodel = new MovieFormViewModel
                {
                    Movie = movieFormViewModel.Movie,
                    Genres = _context.Genres.ToList()
                };
                return View ("MovieForm",viewmodel);
            }

            // file name add in db and pic store in folder
            string path = Server.MapPath("~/Images");
            string fileName = Path.GetFileName(movieFormViewModel.file.FileName);
            string fullPath = Path.Combine(path, fileName);

            // Getting the video path
            string videoFileName = Path.GetFileName(movieFormViewModel.videoFile.FileName);
            
            // for saving new movie
            if (movieFormViewModel.Movie.Id == 0 && movieFormViewModel.file != null)
            {
                movieFormViewModel.Movie.DateAdded = DateTime.Now;

                _context.Movies.Add(movieFormViewModel.Movie);
                _context.SaveChanges();

                int Movieid = movieFormViewModel.Movie.Id;

                //Image Saver
                string relativePath = "/Images/";
                ImageController.SaveImage(relativePath+fileName, Movieid);
                movieFormViewModel.file.SaveAs(fullPath);  // save image in full path

                //saving video path in db
                VideosController.Create(movieFormViewModel.videoFile,Movieid);
                // Saving video in video folder
                movieFormViewModel.videoFile.SaveAs(Server.MapPath("/Videos/" + videoFileName));
            }
            else
            {   // For edit movie
                var movieInDb = _context.Movies.Single(m => m.Id == movieFormViewModel.Movie.Id);
                movieInDb.Name = movieFormViewModel.Movie.Name;
                movieInDb.GenreId = movieFormViewModel.Movie.GenreId;
                movieInDb.NumberInStock = movieFormViewModel.Movie.NumberInStock;
                movieInDb.ReleaseDate = movieFormViewModel.Movie.ReleaseDate;

                //Image Saver
                string relativePath = "/Images/";  // relative path
                ImageController.SaveImage(relativePath+fileName, movieFormViewModel.Movie.Id);
                movieFormViewModel.file.SaveAs(fullPath);  // save image in full path

                //saving video path in db
                VideosController.Create(movieFormViewModel.videoFile, movieFormViewModel.Movie.Id);
                // Saving video in video folder
                movieFormViewModel.videoFile.SaveAs(Server.MapPath("/Videos/" + videoFileName));

                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Movies");
        }

        public ActionResult VideosList()
        {
            if (User.IsInRole("canManageMovies"))
            {
                return View("List");    // =>  For Admin
            }

            // customerName
            // Videos List with videos Address
            // Rent option
            // play option
            CustomerAspNetUser customerAspNetUser = new CustomerAspNetUser();
            string userId = User.Identity.GetUserId();
            try
            {
                customerAspNetUser = _context.customerAspNetUsers.FirstOrDefault(c => c.AspNetUserId == userId);
            }
            catch
            {
                return View("ReadOnlyList");
            }
            

            int customerId = customerAspNetUser.CustomerId;

            var customer = _context.Customers.SingleOrDefault(c => c.Id == customerId);
           IEnumerable<Movie> movies = _context.Movies.Include(m => m.Genre).ToList();

            if (customer == null)
                return View("ReadOnlyList");

            List<ImagesPath> image_path = new List<ImagesPath>();
            foreach (var movie in movies)
            {
                try
                {
                    ImagesPath imagesPaths = _context.ImagesPaths.Where(m => m.MovieId == movie.Id).ToList()[0];
                    image_path.Add(imagesPaths);
                }
                catch
                {
                    ImagesPath imageP = new ImagesPath();
                    imageP.MovieId = movie.Id;
                    imageP.ImagePath = @"\Images\Vidly.png";
                    image_path.Add(imageP);
                }
            }

            dynamic model = new ExpandoObject();
            model.customer = customer;
            model.movies = movies;
            model.imagesPath = image_path;

            return View("VideosList", model);
        }
    }
}