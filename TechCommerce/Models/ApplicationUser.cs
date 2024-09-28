using Microsoft.AspNetCore.Identity;

namespace ThreeMyVersion.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Address { get; set; }
        public string? Mail { get; set; }
        public string? Phone { get; set; }
    }
}
