using System.Net.Http;
using System.Threading.Tasks;
using DotNetEnv;

namespace WeatherApi.Tests.Utils
{
    public static class WeatherApiHelper
    {
        private static readonly string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

        // Loads environment variables and returns API key
        public static string GetApiKey()
        {
            Env.Load();
            var apiKey = Environment.GetEnvironmentVariable("API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("API_KEY environment variable is not set. Please create a .env file.");
            }
            return apiKey;
        }

        // Sends GET request to the OpenWeatherMap API with the given query string
        public static async Task<HttpResponseMessage> SendRequestAsync(string query)
        {
            using var client = new HttpClient();
            return await client.GetAsync($"{BaseUrl}?{query}");
        }
    }
}
