using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please enter customer's name.")]
        [StringLength(255)]
        public string Name { get; set; }
        
        public bool IsSuscribedToNewsLetter { get; set; }
        
        public MembershipType MembershipType { get; set; }  // Navigation Property
        
        [Display(Name="Membership Type")]
        public Byte MembershipTypeId { get; set; }    //  Foreign Key
        
        //[Display(Name="D.O.B. by DataAnnotations")]
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
    }
}