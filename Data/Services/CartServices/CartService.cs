using TVProject.Data.Interfaces;
using TVProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TVProject.Data.Services.CartServices
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {

            var carts = await _unitOfWork.Carts.GetAllAsync(c => c.CartItems);
            return carts.FirstOrDefault(c => c.UserId == userId);
        }

        public async Task AddItemToCartAsync(string userId, CartItem cartItem)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart
                { 
                    UserId = userId,
                    CartItems = new List<CartItem>() 
                };
                await _unitOfWork.Carts.AddAsync(cart);
            }
            cart.CartItems.Add(cartItem);
            await _unitOfWork.CartItems.AddAsync(cartItem); 
            await _unitOfWork.saveAsync();
        }

        public async Task RemoveItemFromCartAsync(string userId, int cartItemId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            var item = cart?.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);

            if (item != null)
            {
                cart.CartItems.Remove(item);
                await _unitOfWork.CartItems.DeleteAsync(cartItemId); 
                await _unitOfWork.saveAsync();
            }
        }

        public async Task UpdateItemQuantityAsync(string userId, int cartItemId, int newQuantity)
        {
            var cart = await GetCartByUserIdAsync(userId);
            var item = cart?.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);

            if (item != null)
            {
                item.Quantity = newQuantity;
                _unitOfWork.CartItems.UpdateAsync(item); 
                await _unitOfWork.saveAsync();
            }
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync(string userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            return cart?.CartItems ?? Enumerable.Empty<CartItem>();
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
             return await _unitOfWork.Movies.GetAllAsync();
        }
    }
}
