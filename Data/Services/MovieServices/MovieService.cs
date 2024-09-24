using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections;
using TVProject.Data.Interfaces;
using TVProject.Data.Services.ElasticSearch;
using TVProject.Models;

namespace TVProject.Data.Services.MovieServices
{
    public class MovieService : IMovieService
	{
        private readonly IUnitOfWork _unitOfWork;
		private readonly ElasticSearchService _elasticSearchService;
		private readonly IDistributedCache _cache;
        public MovieService(IUnitOfWork unitOfWork, ElasticSearchService elasticSearchService,IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
			_elasticSearchService = elasticSearchService;
			_cache = cache;
        }

		public async Task AddAsync(Movie movie)
		{
			await _unitOfWork.Movies.AddAsync(movie);			
			await _unitOfWork.saveAsync();


			var cacheKey = "moviesList";
			await _cache.RemoveAsync(cacheKey);
			await _cache.RemoveAsync($"movie_{movie.Id}");
			
			var movies = await _unitOfWork.Movies.GetAllAsync();
			var serializedMovie = JsonConvert.SerializeObject(movies);

			await _cache.SetStringAsync(cacheKey, serializedMovie, new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
			});
        }

		public async Task DeleteAsync(int id)
		{
			await _unitOfWork.Movies.DeleteAsync(id);
			await _unitOfWork.saveAsync();

			var cacheKey = "moviesList";
			await _cache.RemoveAsync(cacheKey);
            
        }

        public async Task<IEnumerable<Actor>> GetAllActorsAsync()
        {
            return await _unitOfWork.Actors.GetAllAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
		{
			var cacheKey = "moviesList";
			var cachedMovies = await _cache.GetStringAsync(cacheKey);
			if (!string.IsNullOrEmpty(cachedMovies))
			{
				return JsonConvert.DeserializeObject<IEnumerable<Movie>>(cachedMovies);
			}
			var movies = await _unitOfWork.Movies.GetAllAsync(m => m.Cinema, m => m.Actor_Movies);

			var serializedMovies = JsonConvert.SerializeObject(movies);

			await _cache.SetStringAsync(cacheKey, serializedMovies, new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
			});
			return movies;
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
        public async Task syncAllMovies()
        {
			try
			{
				var movies = await GetAllAsync();
				foreach (var movie in movies)
				{
					await _elasticSearchService.IndexMovieAsync(movie);
				}
			}
			catch (Exception ex)
			{

				throw;
			}
        }

        public async Task<IEnumerable<Movie>> SearchMoviesAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<Movie>();
            }

            return await _unitOfWork.Movies.GetAllAsync(m => m.Name.Contains(query));
        }

        
    }
}
