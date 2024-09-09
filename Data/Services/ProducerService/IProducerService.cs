using TVProject.Models;

namespace TVProject.Data.Services.ProducerService
{
    public interface IProducerService
    {
        Task<IEnumerable<Producer>> GetAllAsync();
        Task<Producer> GetByIdAsync(int id);
        Task AddAsync(Producer producer);
        Task UpdateAsync(Producer producer);
        Task DeleteAsync(int id);
    }
}
