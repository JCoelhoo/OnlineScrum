using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineScrum.Models
{
    [Table("Meetings")]
    public class Meeting
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Column("Developer")]
        public string Developer { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Column("ScrumMaster")]
        public string ScrumMaster { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        [DataType(DataType.DateTime)]
        [Required]
        [Display(Name = "Time of Interview")]
        [Column("Time")]
        public DateTime Time { get; set; }

        [Required]
        [Display(Name = "Location of Interview")]
        [Column("Location")]
        public string Location { get; set; }

        [Display(Name = "What was done yesterday?")]
        [Column("YesterdayQuestion")]
        public string YesterdayQuestion { get; set; }

        [Display(Name = "What will be done today?")]
        [Column("TodayQuestion")]
        public string TodayQuestion { get; set; }

        [Display(Name = "What obstacles exist?")]
        [Column("ObstaclesQuestion")]
        public string ObstaclesQuestion { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }

        [Key]
        [Column("MeetingID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeetingID { get; set; }

        [Column("SprintID")]
        public int SprintID { get; set; }

        [Column("ProjectID")]
        public int ProjectID { get; set; }
    }
}