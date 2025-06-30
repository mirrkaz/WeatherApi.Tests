namespace WeatherApi.Tests
{
    public static class TestData
    {
        // Valid test data
        public const string ValidCityLondon = "London";
        public const string ValidCityMunchen = "München";
        public const string ValidCitySaoPaulo = "São Paulo";

        public const double ValidLat = 35.0;
        public const double ValidLon = 139.0;

        // Invalid test data
        public const string InvalidCity = "FakeCityNameThatDoesNotExist";
        public const string InvalidApiKey = "INVALID_KEY";

        // Units
        public const string UnitsMetric = "metric";
    }
}
