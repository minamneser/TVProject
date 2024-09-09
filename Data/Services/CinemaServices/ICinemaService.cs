using TVProject.Models;

namespace TVProject.Data.Services.CinemaServices
{
	public interface ICinemaService
	{
		Task<IEnumerable<Cinema>> GetAllAsync();
		Task<Cinema> GetByIdAsync(int id);
		Task AddAsync(Cinema cinema);
		Task UpdateAsync(Cinema cinema);
		Task DeleteAsync(int id);	
	}
}
