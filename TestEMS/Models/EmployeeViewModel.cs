using System.ComponentModel.DataAnnotations;

namespace TestEMS.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Active")]
        public string IsActive { get; set; }
    }
}
