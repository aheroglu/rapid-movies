using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Presentation.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Presentation.Models.Concrete
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;

        public MovieService(HttpClient httpClient, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _memoryCache = memoryCache;
        }

        public async Task<List<Movie>> GetMovies()
        {
            if (!_memoryCache.TryGetValue("Top100Movies", out List<Movie> movies))
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "[api-key]" },
                        { "X-RapidAPI-Host", "imdb-top-100-movies.p.rapidapi.com" },
                    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    movies = JsonConvert.DeserializeObject<List<Movie>>(body);
                    _memoryCache.Set("Top100Movies", movies, TimeSpan.FromHours(1));
                }
            }

            return movies;
        }

        public async Task<List<Movie>> TopEightMovies()
        {
            if (!_memoryCache.TryGetValue("TopEightMovies", out List<Movie> movies))
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
                    Headers =
                {
                    { "X-RapidAPI-Key", "[api-key]" },
                    { "X-RapidAPI-Host", "imdb-top-100-movies.p.rapidapi.com" },
                },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    movies = JsonConvert.DeserializeObject<List<Movie>>(body).OrderBy(x => x.rank).Take(8).ToList();
                    _memoryCache.Set("TopEightMovies", movies, TimeSpan.FromHours(1));
                }
            }

            return movies;
        }

        public async Task<List<string>> Genres()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
                Headers =
                {
                    { "X-RapidAPI-Key", "[api-key]" },
                    { "X-RapidAPI-Host", "imdb-top-100-movies.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var movieList = JsonConvert.DeserializeObject<List<Movie>>(body);
                var genres = movieList.SelectMany(x => x.genre).Distinct().ToList();
                return genres;
            }

        }

        public async Task<IEnumerable<Movie>> MoviesByGenre(string genre)
        {
            var allMoviews = await GetMovies();
            var moviesByGenre = allMoviews.Where(x => x.genre.Contains(genre));
            return moviesByGenre;
        }
    }
}
