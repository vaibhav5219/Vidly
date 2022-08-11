using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class CustomerAspNetUser
    {
        public int Id { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
        [Display(Name ="AspNetUser")]
        public virtual string ApplicationUserId { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
        [Display(Name ="Customer")]
        public virtual int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; } 

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}