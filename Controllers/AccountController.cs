using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TVProject.Models;

namespace TVProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManger;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManger = userManager;
        }
        public async Task CheckSubscriptionExpiration()
        {
            var users = _userManger.Users.Where(u => u.SubscriptionExpiration <= DateTime.Now).ToList();
            foreach (var user in users)
            {
                user.SubscriptionType = null; 
                await _userManger.UpdateAsync(user);
            }
        }

        [HttpGet]
        public IActionResult SelectSubscription()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SelectSubscription(string subscriptionType)
        {
            var user = await _userManger.GetUserAsync(User);
            user.SubscriptionType = subscriptionType;
            user.SubscriptionExpiration = DateTime.Now.AddMonths(1);
            await _userManger.UpdateAsync(user);

            return RedirectToAction("Index", "Home");
        }
    }
}
