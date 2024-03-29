﻿using System.Collections.Generic;
using System.Web;
using Vidly.Migrations;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public Movie Movie { get; set; }
        public string Title
        {
            get
            {
                if (Movie != null && Movie.Id != 0)
                    return "Edit Movie";

                return "New Movie";
            }
        }
        public Genres Genre { get; internal set; }

        public HttpPostedFileBase file { get; set; }  // image File

        public HttpPostedFileBase videoFile { get; set; }

    }
}