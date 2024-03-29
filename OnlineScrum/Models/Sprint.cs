﻿using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineScrum.Models
{
    public class SprintItem
    {
        public string Sprint { get; set; }
        public string Item { get; set; }
    }

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
        [Display(Name = "Finish Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FinishDate { get; set; }

        [Column("Items")]
        public string Items { get; set; }

        [Column("MeetingInterval")]
        public int MeetingInterval { get; set; }

        [Column("MeetingLocation")]
        [Required(AllowEmptyStrings = false)]
        public string MeetingLocation { get; set; }
    }
}