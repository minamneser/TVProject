using TVProject.Models;

namespace TVProject.Data.Services.ActorServices
{
    public interface IActorService
    {
        Task<IEnumerable<Actor>> GetAllAsync();
        Task<Actor> GetByIdAsync(int id);
        Task AddAsync(Actor actor);
        Task UpdateAsync(Actor actor);
        Task DeleteAsync(int id);
    }
}
