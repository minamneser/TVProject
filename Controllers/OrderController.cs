using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;
using TVProject.Data.Services.CartServices;
using TVProject.Data.Services.OrderServices;
using TVProject.Models;

namespace TVProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        public OrderController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }
        private string GetUserId()
        {
            return User.Identity.Name;
        }
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return View(orders);
        }

        public async Task<IActionResult>Create(Order order)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }
            var cartItems = await _cartService.GetCartItemsAsync(userId);
            if (!cartItems.Any())
            {
                TempData["Message"] = "Your cart is empty.";
                return RedirectToAction("Index", "Cart");
            }
            double totalPrice = 0;
            foreach (var item in cartItems)
            {
                totalPrice += item.Price * item.Quantity;
            }
            if (ModelState.IsValid)
            {
                order.UserId = userId;
                order.TotalPrice = totalPrice;
                order.OrderDate = DateTime.Now;
                order.OrderItems = cartItems.Select(cartItem => new OrderItem
                {
                    MovieId = cartItem.MovieId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Price,
                }).ToList();
                await _orderService.CreateOrderAsync(order);
                return RedirectToAction("Index");
            }
            return View(order);
        }
        [HttpGet]
        public async Task<IActionResult> ReturnToCart()
        {
            var userId = GetUserId();
            await _orderService.RemoveOrderAsync(userId);
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult OrderConfirmation()
        {
            return View();
        }


    }
}
