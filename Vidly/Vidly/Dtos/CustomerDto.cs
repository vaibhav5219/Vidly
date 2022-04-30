using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter customer's name.")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSuscribedToNewsLetter { get; set; }

        // Its domain class and it will create dependencies to domain Model
        public MembershipTypeDto MembershipType { get; set; }  // Navigation Property

        //[Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }    //  Foreign Key

        //[Display(Name="D.O.B. by DataAnnotations")]
        //[Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
    }
}