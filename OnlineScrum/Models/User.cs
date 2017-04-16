using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineScrum.Models
{
    [Table("User")]
    public class User
    {
        [Column("Username")]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; }

        [Key]
        [Column("Email")]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Column("Password")]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        [MinLength(8)]
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        [Column("Role")]
        [HiddenInput(DisplayValue = false)]
        [Required]
        public int Role { get; set; }
    }
}