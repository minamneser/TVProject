using System.Collections;
using TVProject.Models;

namespace TVProject.Data.Services.MovieServices
{
	public interface IMovieService
	{
		Task<IEnumerable<Movie>> GetAllAsync ();
		Task<Movie> GetByIdAsync (int id);
		Task AddAsync(Movie movie);
		Task UpdateAsync(Movie movie);
		Task DeleteAsync(int id);
        Task<IEnumerable<Cinema>> GetAllCinemasAsync();
        Task<IEnumerable<Producer>> GetAllProducersAsync();
        Task<IEnumerable<Actor>> GetAllActorsAsync();
		Task <IEnumerable<Movie>> SearchMoviesAsync(string query);
        Task syncAllMovies();
    }
}
