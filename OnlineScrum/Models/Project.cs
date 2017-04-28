using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineScrum.Models
{
    [Table("Project")]
    public class Project
    {
        [Column("Name")]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Key]
        [Column("ProjectID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectID { get; set; }

        [Column("ScrumMaster")]
        [ForeignKey("User")]
        public string Username { get; set; }

        [Column("DevTeam")]
        [ForeignKey("User")]
        public List<string> DevTeam { get; set; }

        /*
        [Column("Sprint")]
        [ForeignKey("Sprint")]
        public List<int> Sprints { get; set; }

    */
    }
}
 