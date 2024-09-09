using TVProject.Data.Interfaces;
using TVProject.Models;

namespace TVProject.Data.Services.CinemaServices
{
	public class CinemaService : ICinemaService
	{
        private readonly IUnitOfWork _unitOfWork;
        public CinemaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task AddAsync(Cinema cinema)
		{
			await _unitOfWork.Cinemas.AddAsync(cinema);
			await _unitOfWork.saveAsync();
		}

		public async Task DeleteAsync(int id)
		{
			await _unitOfWork.Cinemas.DeleteAsync(id);
			await _unitOfWork.saveAsync();
		}

		public async Task<IEnumerable<Cinema>> GetAllAsync()
		{
			return await _unitOfWork.Cinemas.GetAllAsync();
		}

		public async Task<Cinema> GetByIdAsync(int id)
		{
			return await _unitOfWork.Cinemas.GetByIdAsync(id);
		}

		public async Task UpdateAsync(Cinema cinema)
		{
			await _unitOfWork.Cinemas.UpdateAsync(cinema);
			await _unitOfWork.saveAsync();
		}
	}
}
