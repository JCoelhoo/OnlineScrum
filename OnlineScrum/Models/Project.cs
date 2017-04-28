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
        public virtual string ScrumMaster { get; set; }
        //Message = "The ForeignKeyAttribute on property 'ScrumMaster' on type 'OnlineScrum.Models.Project' is not valid. 
        //The navigation property 'User' was not found on the dependent type 'OnlineScrum.Models.Project'. 
        //The Name value should be a valid navigation property name."

        [Column("DevTeam")]
        [ForeignKey("User")]
        public virtual List<string> DevTeam { get; set; }

        public virtual User User { get; set; }
        /*
        [Column("Sprint")]
        [ForeignKey("Sprint")]
        public List<int> Sprints { get; set; }

    */
    }
}
 