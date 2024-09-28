using System.ComponentModel.DataAnnotations;

namespace ThreeMyVersion.ViewModel
{
    public class LoginUserViewModel
    {

        [Required(ErrorMessage = "*")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name="Remember Me")]
        public bool RememberMe { get; set; }
        
    }
}
