using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetBuddy.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string MatKhau { get; set; }
    }
}