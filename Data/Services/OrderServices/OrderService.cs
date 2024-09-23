using TVProject.Data.Interfaces;
using TVProject.Models;

namespace TVProject.Data.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.saveAsync();
            return order;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.GetAllAsync(o => o.OrderItems);
            return order.FirstOrDefault(o => o.Id == orderId);

        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            var order = await _unitOfWork.Orders.GetAllAsync(o => o.OrderItems);
            return order.Where(o => o.UserId == userId);
        }
        public async Task RemoveOrderAsync(string userId)
        {
            var allorders = await _unitOfWork.Orders.GetAllAsync(o => o.OrderItems);
            var order = allorders.Where(o => o.UserId == userId).ToList();
            if (order != null)
            {
                foreach (var item in order)
                {
                    await _unitOfWork.Orders.DeleteAsync(item.Id);
                }
                await _unitOfWork.saveAsync();
            }
            
        }
    }
}
