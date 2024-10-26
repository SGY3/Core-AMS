using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestEMS.Models
{
    public class ToDo
    {
        [Key]
        public int ToDoId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name ="Project Name")]
        public int ProjectId { get; set; } // Foreign key to Project

        [ForeignKey("ProjectId")]
        public Project? Project { get; set; } // Navigation property for Project

        [Required]
        [Display(Name ="Activity Type")]
        public int ActivityTypeId { get; set; } // Foreign key to ActivityType

        [ForeignKey("ActivityTypeId")]
        public ActivityTypes? ActivityType { get; set; } // Navigation property for ActivityType

        public string? PageName { get; set; }

        [Required]
        public string Priority { get; set; }

        // User who created the ToDo
        public string? CreatedBy { get; set; }

        // Employee the ToDo is assigned to
        public string? AssignedTo { get; set; } // Can be null until assigned

    }
}
