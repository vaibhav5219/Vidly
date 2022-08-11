using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerRegisterViewModel
    {
        public RegisterViewModel registerViewModel { get; set; }

        // For Customer Form Details
        public CustomerFormViewModel customerFormViewModel { get; set; }
    }
}