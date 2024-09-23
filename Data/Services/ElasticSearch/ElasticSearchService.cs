using Microsoft.CodeAnalysis;
using Nest;
using TVProject.Data.Services.MovieServices;
using TVProject.Models;

namespace TVProject.Data.Services.ElasticSearch
{
    public class ElasticSearchService
    {
        private readonly IElasticClient _client;

        public ElasticSearchService(IConfiguration configuration)
        {

            var settings = new ConnectionSettings(new Uri(configuration["Elasticsearch:Uri"]))
                .DefaultIndex("movies")
                .BasicAuthentication(configuration["Elasticsearch:Username"], configuration["Elasticsearch:Password"])
                .ServerCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true);
            _client = new ElasticClient(settings);

        }
        public async Task IndexMovieAsync(Movie movie)
        {
            var response = await _client.IndexDocumentAsync(movie);
        }
        public async Task<ISearchResponse<Movie>> SearchMoviesAsync(string query)
        {
            var searchResponse = await _client.SearchAsync<Movie>(s => s
                .Query(q => q
                    .MultiMatch(m => m
                        .Fields(f => f
                            .Field(p => p.Name)
                            .Field(p => p.Description))
                        .Query(query)
                        .Fuzziness(Fuzziness.Auto)
                    )
                )
            );
            Console.WriteLine($"Total Hits: {searchResponse.Total}");
            return searchResponse;
        }

        public async Task CreateDocument(Movie movie)
        {
            await _client.IndexDocumentAsync<Movie>(movie);
        }
    }
}
