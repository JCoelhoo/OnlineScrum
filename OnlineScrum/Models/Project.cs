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
        [ForeignKey("ScrumMasterUser")]
        public virtual string ScrumMaster { get; set; }
        
        [Column("DevTeam")]
        //[ForeignKey("ScrumMasterUser")]
        public virtual string DevTeam { get; set; }

        public List<string> DevTeamList { get; set; }

        public virtual User ScrumMasterUser { get; set; }
        //public virtual ICollection<User> DevTeamUser { get; set; }
        /*
        [Column("Sprint")]
        [ForeignKey("Sprint")]
        public List<int> Sprints { get; set; }

    */
    }
}
 