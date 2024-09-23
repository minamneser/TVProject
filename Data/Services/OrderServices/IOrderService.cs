using TVProject.Models;

namespace TVProject.Data.Services.OrderServices
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        Task RemoveOrderAsync(string userId);
    }
}
