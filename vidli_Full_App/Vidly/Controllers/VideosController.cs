using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Vidly.Controllers
{
    public class VideosController : Controller
    {
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
