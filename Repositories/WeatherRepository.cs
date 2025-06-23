using MauiAppWR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiAppWR.Repositories
{
    internal class WeatherRepository
    {
        private readonly HttpClient _httpClient;

        public WeatherRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherInfo> GetWeatherAsync(double latitude, double longitude)
        {
            try
            {
                string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,relative_humidity_2m,rain";
                var response = await _httpClient.GetStringAsync(url);

                using JsonDocument doc = JsonDocument.Parse(response);
                var root = doc.RootElement;

                var current = root.GetProperty("current");

                return new WeatherInfo
                {
                    Time = DateTime.Parse(current.GetProperty("time").GetString() ?? DateTime.Now.ToString()),
                    Temperature = current.GetProperty("temperature_2m").GetDouble(),
                    Humidity = current.GetProperty("relative_humidity_2m").GetInt32(),
                    Rain = current.GetProperty("rain").GetDouble()
                };
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error al obtener el clima", ex.Message, "OK");
                return null;
            }
        }

    }
}
