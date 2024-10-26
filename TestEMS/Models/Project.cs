using System.ComponentModel.DataAnnotations;

namespace TestEMS.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        [Required]
        [Display(Name = "Active")]
        public string IsActive { get; set; }
    }
}
