using System.ComponentModel.DataAnnotations;

namespace TechCommerce.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "*")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "*")]
        public string? Mail { get; set; }

        [Required(ErrorMessage = "*")]
        public string? Phone { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
