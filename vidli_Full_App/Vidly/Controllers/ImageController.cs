using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class ImageController
    {
        private static ApplicationDbContext _context;

        public ImageController()
        {
            _context = new ApplicationDbContext();
        }

        public static void SaveImage(String fullPath, HttpPostedFileBase file, int movieId)
        {
            using(var context = new ApplicationDbContext())
            {
                ImagesPath imagesPath = new ImagesPath();
                imagesPath.MovieId = movieId;
                imagesPath.ImagePath = fullPath;

                context.ImagesPaths.Add(imagesPath);
                context.SaveChanges();
                file.SaveAs(fullPath);
            }
        }

    }
}