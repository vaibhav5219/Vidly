using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Dtos;

namespace Vidly.Controllers.Api
{
    public class CustomersDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

        public MembershipTypeDto MembershipType { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? Birthdate { get; set; }
    }
}