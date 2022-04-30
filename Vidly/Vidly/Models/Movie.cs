using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Range(1,20)]
        public int NumberInStocks { get; set; }
        
        [Required]
        public DateTime? DateAdded { get; set; }
        
        public Genre Genre { get; set; } // navigation property
        
        [Required]
        public int GenreId { get; set; }  // ForeignKey
        
    }
}