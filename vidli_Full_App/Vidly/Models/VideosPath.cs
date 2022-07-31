using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class VideosPath
    {
        public int id { get; set; }

        public int MovieId { get; set; }

        public string VideoPath { get; set; }
    }
}