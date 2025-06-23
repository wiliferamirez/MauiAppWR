using MauiAppWR.ViewModels;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiAppWR.Views;

public partial class WeatherPage : ContentPage
{
	public WeatherPage()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var vm = BindingContext as WeatherViewModel;
        await ((AsyncRelayCommand)vm.LoadWeatherCommand).ExecuteAsync(null);
    }
}