using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Vidly.Models;

namespace Vidly.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        /*[OutputCache(Duration =0,  VaryByParam ="*", NoStore =true)]  // Not storing catch duration 0 second
        public ActionResult Index()
        {
            return View();
        }*/

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Index()
        {
            IEnumerable<Movie> movies = _context.Movies.ToList();

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
            model.movies = movies;
            model.imagesPath = image_path;

            return View("Index",model);
        }
    }
}