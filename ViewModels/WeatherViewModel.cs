using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppWR.Models;
using MauiAppWR.Repositories;
using System.Windows.Input;
using Microsoft.Maui.Devices.Sensors;

namespace MauiAppWR.ViewModels
{
    internal partial class WeatherViewModel : ObservableObject
    {
        private readonly WeatherRepository _repository;

        [ObservableProperty]
        private WeatherInfo clima;

        public ICommand LoadWeatherCommand { get; }

        public WeatherViewModel()
        {
            _repository = new WeatherRepository();
            LoadWeatherCommand = new AsyncRelayCommand(LoadWeatherAsync);
        }

        private async Task LoadWeatherAsync()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    await Shell.Current.DisplayAlert("Permiso denegado", "Se necesita tu ubicación para mostrar el clima.", "OK");
                    return;
                }
            }
            var location = await Geolocation.GetLastKnownLocationAsync()
                           ?? await Geolocation.GetLocationAsync(new GeolocationRequest());

            if (location != null)
            {
                clima = await _repository.GetWeatherAsync(location.Latitude, location.Longitude);
            }
        }
    }
}
