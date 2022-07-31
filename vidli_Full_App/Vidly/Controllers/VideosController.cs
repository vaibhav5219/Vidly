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

namespace Vidly.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("/Videos")]
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
            if (id == null || id == 0)
                return HttpNotFound();

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
