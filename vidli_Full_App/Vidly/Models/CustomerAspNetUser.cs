using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class CustomerAspNetUser
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(450)]
        [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
        public string AspNetUserId { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
        public int CustomerId { get; set; }
    }
}