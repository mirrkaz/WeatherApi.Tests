using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApi.Tests.Utils;
using System.Net;

namespace WeatherApi.Tests
{
    [TestFixture]
    public class WeatherApiTests
    {
        private string? _apiKey;

        [SetUp]
        public void Setup()
        {
            _apiKey = WeatherApiHelper.GetApiKey();
        }

        [Test]
        [Category("Positive")]
        [Description("Test that valid latitude and longitude returns HTTP 200 OK.")]
        public async Task ValidLatLon_ShouldReturn200()
        {
            var response = await WeatherApiHelper.SendRequestAsync($"lat={TestData.ValidLat}&lon={TestData.ValidLon}&appid={_apiKey}");
            TestContext.WriteLine($"Response Code: {(int)response.StatusCode}");
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [Test]
        [Category("Negative")]
        [Description("Test that invalid API key returns HTTP 401 Unauthorized.")]
        public async Task InvalidApiKey_ShouldReturn401()
        {
            var response = await WeatherApiHelper.SendRequestAsync($"lat={TestData.ValidLat}&lon={TestData.ValidLon}&appid={TestData.InvalidApiKey}");
            TestContext.WriteLine($"Response Code: {(int)response.StatusCode}");
            Assert.AreEqual(401, (int)response.StatusCode);
        }

        [Test]
        [Category("Positive")]
        [Description("Test that valid city name returns HTTP 200 and the correct city in the response.")]
        public async Task ValidCityName_ShouldReturn200AndCorrectCity()
        {
            string cityName = TestData.ValidCityLondon;

            var response = await WeatherApiHelper.SendRequestAsync($"q={WebUtility.UrlEncode(cityName)}&appid={_apiKey}");
            TestContext.WriteLine($"Response Code: {(int)response.StatusCode}");
            Assert.AreEqual(200, (int)response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            dynamic? data = JsonConvert.DeserializeObject(json);
            Assert.IsNotNull(data, "Deserialized JSON is null.");
            Assert.AreEqual(cityName, (string)data.name);
        }

        [Test]
        [Category("Negative")]
        [Description("Test that an invalid city name returns HTTP 404 Not Found.")]
        public async Task InvalidCityName_ShouldReturn404()
        {
            var response = await WeatherApiHelper.SendRequestAsync($"q={WebUtility.UrlEncode(TestData.InvalidCity)}&appid={_apiKey}");
            TestContext.WriteLine($"Response Code: {(int)response.StatusCode}");
            Assert.AreEqual(404, (int)response.StatusCode);
        }

        [Test]
        [Category("Functional")]
        [Description("Test that using metric units returns temperature in Celsius within plausible range.")]
        public async Task UnitsMetric_ShouldReturnTemperatureInCelsiusRange()
        {
            var response = await WeatherApiHelper.SendRequestAsync($"q={WebUtility.UrlEncode(TestData.ValidCityLondon)}&units={TestData.UnitsMetric}&appid={_apiKey}");
            TestContext.WriteLine($"Response Code: {(int)response.StatusCode}");
            Assert.AreEqual(200, (int)response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            dynamic? data = JsonConvert.DeserializeObject(json);
            Assert.IsNotNull(data, "Deserialized JSON is null.");
            double temp = (double)data.main.temp;

            Assert.IsTrue(temp > -50 && temp < 60, $"Temperature {temp}°C is out of expected range");
        }

        [Test]
        [Category("Positive")]
        [Description("Test that city names with special UTF-8 characters return HTTP 200 and correct city name.")]
        public async Task CityNameWithSpecialCharacters_ShouldReturn200AndCorrectCity()
        {
            string cityName = TestData.ValidCityMunchen;

            var response = await WeatherApiHelper.SendRequestAsync($"q={WebUtility.UrlEncode(cityName)}&appid={_apiKey}");
            TestContext.WriteLine($"Response Code: {(int)response.StatusCode}");
            Assert.AreEqual(200, (int)response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            dynamic? data = JsonConvert.DeserializeObject(json);
            Assert.IsNotNull(data, "Deserialized JSON is null");

            string returnedCityName = (string)data.name;
            TestContext.WriteLine($"Returned City Name: {returnedCityName}");

            Assert.That(returnedCityName, Does.Contain("Munchen").IgnoreCase.Or.Contain("München"));
        }
    }
}
