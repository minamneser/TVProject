using System.Collections;
using TVProject.Data.Interfaces;
using TVProject.Models;

namespace TVProject.Data.Services.MovieServices
{
	public class MovieService : IMovieService
	{
        private readonly IUnitOfWork _unitOfWork;
        public MovieService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task AddAsync(Movie movie)
		{
			await _unitOfWork.Movies.AddAsync(movie);
			await _unitOfWork.saveAsync();
		}

		public async Task DeleteAsync(int id)
		{
			await _unitOfWork.Movies.DeleteAsync(id);
			await _unitOfWork.saveAsync();
		}

        public async Task<IEnumerable<Actor>> GetAllActorsAsync()
        {
            return await _unitOfWork.Actors.GetAllAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
		{
			return await _unitOfWork.Movies.GetAllAsync(m => m.Cinema, m => m.Actor_Movies);
		}

        public async Task<IEnumerable<Cinema>> GetAllCinemasAsync()
        {
			return await _unitOfWork.Cinemas.GetAllAsync();
        }

        public async Task<IEnumerable<Producer>> GetAllProducersAsync()
        {
            return await _unitOfWork.Producers.GetAllAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
		{
			return await _unitOfWork.Movies.GetByIdAsync(id);
		}

		public async Task UpdateAsync(Movie movie)
		{
			await _unitOfWork.Movies.UpdateAsync(movie);
			await _unitOfWork.saveAsync();
		}
	}
}
