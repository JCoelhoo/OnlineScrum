using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineScrum.Models
{
    [Table("Project")]
    public class Project
    {
        [Column("Name")]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Project Name")]
        public string Name { get; set; }

        [Key]
        [Column("ProjectID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectID { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("ScrumMaster")]
        [ForeignKey("ScrumMasterUser")]
        public virtual string ScrumMaster { get; set; }
        
        public virtual User ScrumMasterUser { get; set; }

        [Column("DevTeam")]
        public virtual string DevTeam { get; set; }
        [Display(Name = "Development Team")]
        public List<string> DevTeamList { get; set; }
        
        [Column("Sprint")]
        public string Sprints { get; set; }
    }
}
 