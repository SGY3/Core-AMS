using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestEMS.Models
{
    public class MyActivity
    {

        [Required]
        [Key]
        public string ActivityId { get; set; }
        [Display(Name ="ToDo Id")]
        public string? ToDoId { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string ActivityTitle { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string ActivityDescription { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        public int ProjectId { get; set; } // Foreign key to Project

        [Display(Name = "Project Name")]
        [ForeignKey("ProjectId")]
        public virtual Project? Project { get; set; } // Navigation property for Project

        [Required]
        public int ActivityTypeId { get; set; } // Foreign key to ActivityType
        [Display(Name = "Activity Type")]

        [ForeignKey("ActivityTypeId")]
        public virtual ActivityTypes? ActivityType { get; set; } // Navigation property for ActivityType
        [Display(Name = "Page Name")]
        public string? PageName { get; set; }

        [Required]
        public string? Priority { get; set; }
        public int? AssignedTo { get; set; } // Can be null until assigned
        [ForeignKey("AssignedTo"), Display(Name = "Assigned To")]
        public virtual EmployeeData? AssignedToEmployeeData { get; set; }
        public int? AssignedBy { get; set; } // Can be null until assigned
        [ForeignKey("AssignedBy"), Display(Name = "Assigned By")]
        public virtual EmployeeData? AssignedByEmployeeData { get; set; }
        [Display(Name = "Assigned Date")]
        public DateTime? AssignedDate { get; set; }
    }
}
