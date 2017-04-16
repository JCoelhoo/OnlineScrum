using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineScrum.Models
{
    public class Register
    {
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; }

        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(50)]
        [MinLength(8)]
        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfimrPassword { get; set; }
    }
}