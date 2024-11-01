﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestEMS.Models
{
    public class ToDo
    {
        [Key]
        [Required]
        public string ToDoId { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]

        public string Description { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        public int ProjectId { get; set; } // Foreign key to Project

        [ForeignKey("ProjectId")]
        public virtual Project? Project { get; set; } // Navigation property for Project

        [Required]
        [Display(Name = "Activity Type")]
        public int ActivityTypeId { get; set; } // Foreign key to ActivityType

        [ForeignKey("ActivityTypeId")]
        public virtual ActivityTypes? ActivityType { get; set; } // Navigation property for ActivityType
        [Display(Name = "Page Name")]
        public string? PageName { get; set; }

        [Required]
        public string? Priority { get; set; }

        // User who created the ToDo
        [Display(Name = "Created By")]
        public int? CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual EmployeeData? AddedByEmployeeData { get; set; }

        // Employee the ToDo is assigned to
        public int? AssignedTo { get; set; } // Can be null until assigned
        [ForeignKey("AssignedTo")]
        public virtual EmployeeData? AssignedByEmployeeData { get; set; }

    }
}
