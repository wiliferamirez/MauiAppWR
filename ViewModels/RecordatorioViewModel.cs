using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppWR.Models;
using System.Windows.Input;

namespace MauiAppWR.ViewModels
{
    internal class RecordatorioViewModel : ObservableObject, IQueryAttributable
    {
        private Recordatorio _recordatorio;

        public string Texto
        {
            get => _recordatorio.Texto;
            set
            {
                if (_recordatorio.Texto != value)
                {
                    _recordatorio.Texto = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime FechaHora
        {
            get => _recordatorio.FechaHora;
            set
            {
                if (_recordatorio.FechaHora != value)
                {
                    _recordatorio.FechaHora = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Activo
        {
            get => _recordatorio.Activo;
            set
            {
                if (_recordatorio.Activo != value)
                {
                    _recordatorio.Activo = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand GuardarCommand { get; }

        public RecordatorioViewModel()
        {
            _recordatorio = new Recordatorio();
            GuardarCommand = new AsyncRelayCommand(GuardarAsync);
        }

        private async Task GuardarAsync()
        {
            var lista = Recordatorio.LoadAll();
            lista.Insert(0, _recordatorio);
            Recordatorio.SaveAll(lista);
            await Shell.Current.GoToAsync($"..?guardado={_recordatorio.Id}");
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // Para edición futura
        }
    }
}
