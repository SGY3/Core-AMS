using System.ComponentModel.DataAnnotations;

namespace TestEMS.Models
{
    public class MyActivityViewModel
    {
        public string ActivityId { get; set; }
        [Display(Name = "Activity Id")]
        public string DisplayActivityId { get; set; }

        [Display(Name = "Title")]
        public string ActivityTitle { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string ActivityDescription { get; set; }
    }
}
