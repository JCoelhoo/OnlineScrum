using Foolproof;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        [Display(Name = "Sprint Name")]
        public string SprintName { get; set; }

        [Column("StartDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Start Date")]
        [Required]
        public DateTime StartDate { get; set; }

        [Column("FinishDate")]
        [DataType(DataType.Date)]
        [GreaterThan("StartDate")]
        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FinishDate { get; set; }

        [Column("Items")]
        public string Items { get; set; }

        [Column("Meetings")]
        public string Meetings { get; set; }
    }
}