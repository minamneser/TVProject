using TVProject.Models;

namespace TVProject.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Actor> Actors { get; }
        IGenericRepository<Producer> Producers { get; }
        IGenericRepository<Cinema> Cinemas { get; }
        IGenericRepository<Movie> Movies { get; }
        IGenericRepository<Cart> Carts { get; }
        IGenericRepository<CartItem> CartItems { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<OrderItem> OrdersItems { get; }


        Task saveAsync();
        
    }
}
