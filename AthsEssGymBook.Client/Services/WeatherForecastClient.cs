using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthsEssGymBook.Client.Services
{
    using AthsEssGymBook.Shared;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class WeatherForecastClient
    {
        private readonly HttpClient client;

        public WeatherForecastClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<WeatherForecast[]> GetForecastAsync()
        {
            var forecasts = new WeatherForecast[0];

            try
            {
                forecasts = await client.GetFromJsonAsync<WeatherForecast[]>(
                    "api/SampleData/WeatherForecasts");
            }
            catch
            {
              
            }

            return forecasts;
        }
    }
}
