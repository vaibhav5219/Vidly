using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class ImagesPath
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public string ImagePath { get; set; }
    }
}