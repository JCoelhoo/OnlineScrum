using System.ComponentModel.DataAnnotations;

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
        [MinLength(8, ErrorMessage = "Password must be over 8 characters")]
        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}