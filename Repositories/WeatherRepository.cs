using MauiAppWR.Models;
using System.Text.Json;

namespace MauiAppWR.Repositories
{
    internal class WeatherRepository
    {
        private readonly HttpClient _httpClient = new();

        public async Task<WeatherInfo> GetWeatherAsync(double latitude, double longitude)
        {
            try
            {
                string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,relative_humidity_2m,rain";
                var response = await _httpClient.GetStringAsync(url);
                using var doc = JsonDocument.Parse(response);
                var current = doc.RootElement.GetProperty("current");

                return new WeatherInfo
                {
                    Time = DateTime.Parse(current.GetProperty("time").GetString()!),
                    Temperature = current.GetProperty("temperature_2m").GetDouble(),
                    Humidity = current.GetProperty("relative_humidity_2m").GetInt32(),
                    Rain = current.GetProperty("rain").GetDouble()
                };
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo obtener el clima.\n{ex.Message}", "OK");
                return null;
            }
        }
    }
}
