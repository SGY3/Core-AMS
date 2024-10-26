using System.ComponentModel.DataAnnotations;

namespace TestEMS.Models
{
    public class ActivityTypes
    {
        [Key]
        public int ActivityId { get; set; }
        [Required]
        [Display(Name = "Activity Type")]
        public string ActivityName { get; set; }
        [Required]
        [Display(Name = "Active")]
        public string IsActive { get; set; }
    }
}
