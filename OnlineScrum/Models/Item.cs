﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineScrum.Models
{
    [Table("Items")]
    public class Item
    {
        [Key]
        [Column("ItemID")]
        public int ItemID { get; set; }

        [Column("Status")]
        public string ItemStatus { get; set; }

        [Column("Number")]
        public int ItemNumber { get; set; }

        [Column("Name")]
        [Display(Name = "Item Name")]
        [Required(AllowEmptyStrings = false)]
        public string ItemName { get; set; }

        [Column("AssignedTo")]
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Needs to be an Email Adress")]
        [Display(Name = "Assign Item")]
        public string AssignedTo { get; set; }

        [Column("EstimatedEffort")]
        [Range(1,5)]
        [Required]
        [Display(Name = "Estimated Effort")]
        public int EstimatedEffort { get; set; }
    }
}