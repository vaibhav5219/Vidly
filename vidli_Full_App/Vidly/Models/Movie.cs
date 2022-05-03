
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        public Genre Genre { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public byte GenreId { get; set; }

        public DateTime? DateAdded { get; set; }

        [Display(Name = "Release Date")]
        
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Number in Stock")]
        [Required]
        [Range(1,20)]
        public byte NumberInStock { get; set; }
        
        public byte NumberAvailable { get; set; }

        public static implicit operator List<object>(Movie v)
        {
            throw new NotImplementedException();
        }
    }
}