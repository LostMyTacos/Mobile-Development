using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using MyMovieApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyMovieApp.Services
{
    class MovieService
    {
        private const string Url = "https://api.themoviedb.org/3/";
        // Need to get a personal use API key from https://www.themoviedb.org/
        private const string Key = "{Insert Your API Key Here}";

        public const int MinSearchLength = 5;

        private HttpClient _client = new HttpClient();

        public async Task<IEnumerable<Movie>> FindMoviesByName(string search)
        {
            var response = await _client.GetStringAsync($"{Url}search/movie/?api_key={Key}&query={search}");
            var responseObject = JObject.Parse(response);
            return JsonConvert.DeserializeObject<IEnumerable<Movie>>(responseObject["results"].ToString());
        }

        public async Task<IEnumerable<Movie>> GetMovie(string id)
        {
            var response = await _client.GetAsync($"{Url}/movie/{id}/{Key}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            var responseObject = JObject.Parse(content);
            return JsonConvert.DeserializeObject<IEnumerable<Movie>>(responseObject["results"].ToString());
        }
    }
}
