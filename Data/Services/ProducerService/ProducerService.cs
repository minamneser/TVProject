using TVProject.Data.Interfaces;
using TVProject.Models;

namespace TVProject.Data.Services.ProducerService
{
    public class ProducerService : IProducerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProducerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Producer producer)
        {
            await _unitOfWork.Producers.AddAsync(producer);
            await _unitOfWork.saveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Producers.DeleteAsync(id);
            await _unitOfWork.saveAsync();
        }

        public async Task<IEnumerable<Producer>> GetAllAsync()
        {
            return await _unitOfWork.Producers.GetAllAsync();
        }

        public async Task<Producer> GetByIdAsync(int id)
        {
            return await _unitOfWork.Producers.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Producer producer)
        {
            await _unitOfWork.Producers.UpdateAsync(producer);
            await _unitOfWork.saveAsync();
        }
    }
}
