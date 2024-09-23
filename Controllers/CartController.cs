using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Exchange.WebServices.Data;
using TVProject.Data.Services.CartServices;
using TVProject.Data.Services.MovieServices;
using TVProject.Models;

namespace TVProject.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IMovieService _movieService;   
        private readonly UserManager<IdentityUser> _userManager;
        public CartController(ICartService cartService, IMovieService movieService,UserManager<IdentityUser> userManager)
        {
            _cartService = cartService;
            _movieService = movieService;
            _userManager = userManager;
        }
        private string GetUserId()
        {
            return User.Identity.Name;
        }
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            ViewBag.Movies = new SelectList(await _cartService.GetAllMoviesAsync(), "Id", "Name");

            if (cart == null)
            {
                cart = new Cart
                {
                    CartItems = new List<CartItem>()
                };
            }

            return View(cart.CartItems);
        }
        [HttpPost]
        public async Task<IActionResult> Add(int movieId, int quantity)
        {
            var userId = GetUserId();
            var user = await _userManager.GetUserAsync(User) as ApplicationUser;

            if (user == null)
            {

                return RedirectToPage("/Account/Login", new { area = "Identity" });

            }

            if (user.SubscriptionType == "Silver")
            {
                
                var cartItems = await _cartService.GetCartItemsAsync(userId);
                if (cartItems.Any())
                {
                    TempData["ErrorMessage"] = "Silver members can only have one item in the cart.";
                    return RedirectToAction("index", "Movies");
                }
                quantity = 1;
            }

            var movie = await _movieService.GetByIdAsync(movieId);
            if (movie == null)
            {
                ModelState.AddModelError("", "Movie not found.");
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                var cartItem = new CartItem
                {
                    Movie = movie,
                    MovieId = movieId,
                    Quantity = quantity,
                    Price = (double)movie.Price,
                };

                await _cartService.AddItemToCartAsync(userId, cartItem);
                return RedirectToAction("Index");
            }

            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var cartItems = await _cartService.GetCartItemsAsync(userId);
            var cartItem = cartItems.FirstOrDefault(ci => ci.Id == id);
            ViewBag.Movies = new SelectList(await _cartService.GetAllMoviesAsync(),"Id","Name");
            if (cartItem == null)
            {
                return NotFound();
            }
            return View(cartItem);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = GetUserId();
            
            await _cartService.RemoveItemFromCartAsync(userId,id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ProceedToOrder()
        {
            var userId = User.Identity?.Name; 
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); 
            }

            var cartItems = await _cartService.GetCartItemsAsync(userId);
            if (!cartItems.Any())
            {
                TempData["Message"] = "Your cart is empty.";
                return RedirectToAction("Index");
            }            
            return RedirectToAction("Create", "Order"); 
        }

    }
}
