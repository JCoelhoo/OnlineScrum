using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineScrum.Models
{
    [Table("Sprint")]
    public class Sprint
    {
        [Key]
        [Column("SprintID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SprintID { get; set; }

        [Column("SprintNumber")]
        public int SprintNumber { get; set; }

        [Column("Name")]
        public string SprintName { get; set; }

        [Column("StartDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [Column("FinishDate")]
        [DataType(DataType.Date)]
        [GreaterThan("StartDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FinishDate { get; set; }

        [Column("Items")]
        public string Items { get; set; }

        //todo remove this
        public List<string> ItemsList { get; set; }
    }
}