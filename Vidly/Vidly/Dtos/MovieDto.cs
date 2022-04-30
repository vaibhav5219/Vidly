using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Range(1, 20)]
        public int NumberInStocks { get; set; }

        [Required]
        public DateTime? DateAdded { get; set; }

        public GenreDto Genre { get; set; } // navigation property

        [Required]
        public int GenreId { get; set; }  // ForeignKey

    }
}