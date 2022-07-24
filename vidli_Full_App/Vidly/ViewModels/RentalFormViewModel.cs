using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class RentalFormViewModel
    {
        //public IEnumerable<Customer> Customers { get; set; }
        //public IEnumerable<Movie> Movies { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        public string SelectedCustomerID { get; set; }
        public IEnumerable<SelectListItem> Customers { get; set; }

        [Required]
        [Display( Name="Movie Name")]
        public string SelectedMovieID { get; set; }
        public IEnumerable<SelectListItem> Movies { get; set; }
    }
}