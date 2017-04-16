using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineScrum.Models
{
    public class Login
    {
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Address is required")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}