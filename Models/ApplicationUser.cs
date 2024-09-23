using Microsoft.AspNetCore.Identity;

namespace TVProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string SubscriptionType { get; set; } 
        public DateTime SubscriptionExpiration { get; set; }
        
    }

    
}
