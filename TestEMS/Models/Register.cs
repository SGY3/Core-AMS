using System.ComponentModel.DataAnnotations;

namespace TestEMS.Models
{
    public class Register
    {
        [Required]
        [Display(Name ="Employee Id")]
        public string EmployeeId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabets are allowed.")]
        [Display(Name = "Enter Full Name")]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password do not match")]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
