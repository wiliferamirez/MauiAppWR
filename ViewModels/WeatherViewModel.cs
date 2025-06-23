using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppWR.Models;
using MauiAppWR.Repositories;
using Microsoft.Maui.Devices.Sensors;

namespace MauiAppWR.ViewModels
{
    internal partial class WeatherViewModel : ObservableObject
    {
        private readonly WeatherRepository _repository = new();

        [ObservableProperty]
        private WeatherInfo clima;

        [RelayCommand]
        public async Task LoadWeatherAsync()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                    if (status != PermissionStatus.Granted)
                    {
                        await Shell.Current.DisplayAlert("Permiso", "Ubicación denegada.", "OK");
                        return;
                    }
                }

                var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));
                if (location == null)
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo obtener la ubicación", "OK");
                    return;
                }

                Clima = await _repository.GetWeatherAsync(location.Latitude, location.Longitude);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Algo salió mal: {ex.Message}", "OK");
            }
        }
    }
}
