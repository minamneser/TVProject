using Microsoft.AspNetCore.Identity;

namespace TVProject.Models
{
    public class SubscriptionChecker
    {
        private readonly UserManager<IdentityUser> _userManager;

        public SubscriptionChecker(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task CheckSubscriptionExpiration()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                var appUser = user as ApplicationUser;
                if (appUser != null && appUser.SubscriptionExpiration < DateTime.UtcNow)
                {
                    appUser.SubscriptionType = null;
                    await _userManager.UpdateAsync(appUser);
                }
            }
        }
    }
}
