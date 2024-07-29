using Microsoft.AspNetCore.Identity;

namespace ShopApplication.Models
{
    public class AppUser : IdentityUser
    {
        public string? Address  { get; set; }
    }
}
