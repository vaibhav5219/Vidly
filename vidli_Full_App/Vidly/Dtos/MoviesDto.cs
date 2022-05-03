using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Dtos;

namespace Vidly.Controllers.Api
{
    public class MoviesDto
    {
        public int Id { get; set; }


        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        

        [Display(Name = "Genre")]
        [Required]
        public byte GenreId { get; set; }

        public DateTime? DateAdded { get; set; }

        [Display(Name = "Release Date")]

        public DateTime? ReleaseDate { get; set; }
        public GenreDto Genre { get; set; }

        [Display(Name = "Number in Stock")]
        [Required]
        [Range(10, 20)]
        public byte NumberInStock { get; set; }
    }
}