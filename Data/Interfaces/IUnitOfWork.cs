using TVProject.Models;

namespace TVProject.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Actor> Actors { get; }
        IGenericRepository<Producer> Producers { get; }
        IGenericRepository<Cinema> Cinemas { get; }
        IGenericRepository<Movie> Movies { get; }
        Task saveAsync();
    }
}
