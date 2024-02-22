using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Reel_Jet.Services.WebServices {
    public static class OmdbService {

        private static readonly HttpClient _client = new HttpClient();

        private static readonly string _key = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()["OmdbKey"]!;

        public static async Task<string> GetAllMoviesByTitle(string title) {
            string url = $"http://www.omdbapi.com/?apikey={_key}&s={title}";
            var task = await _client.GetStringAsync(url);
            return task;
        }

        public static async Task<string> GetConcreteMovieById(string imdbId) {
            string url = $"http://www.omdbapi.com/?apikey={_key}&i={imdbId}&plot=full";
            var task = await _client.GetStringAsync(url);
            return task;
        }

        public static async Task<string> GetConcreteMovieByTitle(string title) {
            string url = $"http://www.omdbapi.com/?apikey={_key}&t={title}&plot=full";
            var task = await _client.GetStringAsync(url);
            return task;
        }

    }
}
