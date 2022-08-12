using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Vidly.Models;
using System.Dynamic;
using Microsoft.AspNet.Identity;

namespace Vidly.Controllers
{
    [RoutePrefix("/Videos")]
    [AllowAnonymous]
    public class VideosController : Controller
    {
        private ApplicationDbContext _context;

        public VideosController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Route("/ShowVideo/{id}")]
        public ActionResult ShowVideo(int? id)
        {
            if ( !(User.IsInRole("canManageMovies") || User.IsInRole("isAcustomer")) )
            {
                return RedirectToAction("Login","Account", new { area = "" });
            }

            if (id == null || id == 0)
                return HttpNotFound();

            if (User.IsInRole("isAcustomer"))
            {
                // ydi ye video id customer ne book kiya hai to ye video dekh skta hai else not
                CustomerAspNetUser customerAspNetUser = new CustomerAspNetUser();
                int customerId;
                var customer = new Customer();
                Rental rental = new Rental();
                VideosPath videosPath = new VideosPath();

                string userId = User.Identity.GetUserId();
                try
                {
                    customerAspNetUser = _context.customerAspNetUsers.FirstOrDefault(c => c.ApplicationUserId == userId);
                    customerId = customerAspNetUser.CustomerId;

                    customer = _context.Customers.SingleOrDefault(c => c.Id == customerId);
                    videosPath = _context.videosPaths.Where(m => m.id == id).ToList()[0];
                    rental = _context.Rentals.SingleOrDefault(c => c.Customer.Id == customerId && c.Movie.Id == videosPath.MovieId );
                }
                catch
                {
                    return RedirectToAction("Index", "Movies");
                }

                //IEnumerable<Rental> rentals = _context.Rentals.Include(m => m.Movie).Include(m => m.Customer).ToList().Where(r => r.Customer.Id == customer.Id && r.Movie.Id==id);
                //IEnumerable<Movie> movies = _context.Movies.Include(m => m.Genre).Where(m => m.Id == id);
                if (customer == null)
                    return HttpNotFound();

                if(rental == null) 
                    videosPath.VideoPath = @"\Videos\1.mp4";

                dynamic model = new ExpandoObject();
                model.customer = customer;
                ViewBag.VideosPaths = videosPath;

                return View("Index");
            }

            List<VideosPath> videosPaths = new List<VideosPath>();

            // Fetching video path in database using C#
            string mainConn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlConn = new SqlConnection(mainConn);

            string sqlQuery = "SELECT * from [dbo].[VideosPaths] where MovieId="+id;
            SqlCommand sqlComm = new SqlCommand(sqlQuery, sqlConn);

            sqlConn.Open();
            SqlDataReader sqlDataReader = sqlComm.ExecuteReader();
            while(sqlDataReader.Read())
            {
                VideosPath videosPath = new VideosPath();
                videosPath.id = Convert.ToInt32(sqlDataReader["Id"].ToString());
                videosPath.MovieId = Convert.ToInt32(sqlDataReader["MovieId"]);
                videosPath.VideoPath = sqlDataReader["VideoPath"].ToString();
                videosPaths.Add(videosPath);
            }
            sqlConn.Close();

            if(videosPaths == null || videosPaths.Count==0)
            {
                return HttpNotFound();
            }

            ViewBag.VideosPaths = videosPaths[0];

            return View("Index");
        }
        public static void Create(HttpPostedFileBase videoFile, int movieId)
        {
            if(videoFile != null)
            {
                string fileName = Path.GetFileName(videoFile.FileName);
                if(videoFile.ContentLength < 104857600)  // 100MB => 104857600 bytes    2,147,483,647
                { 
                    // Saving video path in database using C#
                    string mainConn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection sqlConnection = new SqlConnection(mainConn);

                    string sqlQuery = "insert into [dbo].[VideosPaths] (MovieId,VideoPath) values (@MovieId,@VideoPath)";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery,sqlConnection);

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@MovieId", movieId);
                    sqlCommand.Parameters.AddWithValue("@VideoPath", "/Videos/" + fileName);
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }
    }
}
