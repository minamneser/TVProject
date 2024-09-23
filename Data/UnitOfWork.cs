using TVProject.Data.DataBase;
using TVProject.Data.Interfaces;
using TVProject.Data.Repository;
using TVProject.Models;

namespace TVProject.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Actors = new GenericRepository<Actor>(_context);
            Producers = new GenericRepository<Producer>(_context);
            Cinemas = new GenericRepository<Cinema>(_context);
            Movies = new GenericRepository<Movie>(_context);
            Carts = new GenericRepository<Cart>(_context);
            CartItems = new GenericRepository<CartItem>(_context);
            Orders = new GenericRepository<Order>(_context);
            OrdersItems = new GenericRepository<OrderItem>(_context);
            
        }

        public IGenericRepository<Actor> Actors { get; private set; }
        public IGenericRepository<Producer> Producers { get; private set; }
        public IGenericRepository<Cinema> Cinemas { get; private set; }

		public IGenericRepository<Movie> Movies {  get; private set; }
        public IGenericRepository<Cart> Carts { get; private set; }
        public IGenericRepository<CartItem> CartItems { get; private set; }
        public IGenericRepository<Order> Orders { get; private set; }
        public IGenericRepository<OrderItem> OrdersItems { get; private set; }

        public async Task saveAsync()
        {
            await _context.SaveChangesAsync();
        }

        
    }
}
