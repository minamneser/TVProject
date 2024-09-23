using TVProject.Models;

namespace TVProject.Data.Services.CartServices
{
    public interface ICartService
    {
        Task<Cart> GetCartByUserIdAsync(string userId);
        Task AddItemToCartAsync(string userId, CartItem cartItem);
        Task RemoveItemFromCartAsync(string userId, int cartItemId);
        Task UpdateItemQuantityAsync(string userId, int cartItemId, int newQuantity);
        Task<IEnumerable<CartItem>> GetCartItemsAsync(string userId);
        Task<IEnumerable<Movie>> GetAllMoviesAsync();

    }
}
