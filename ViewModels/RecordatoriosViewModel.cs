using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppWR.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiAppWR.ViewModels
{
    internal class RecordatoriosViewModel : ObservableObject
    {
        public ObservableCollection<Recordatorio> TodosLosRecordatorios { get; set; }

        public ICommand NuevoCommand { get; }
        public ICommand EliminarCommand { get; }

        public RecordatoriosViewModel()
        {
            TodosLosRecordatorios = new ObservableCollection<Recordatorio>(Recordatorio.LoadAll());
            NuevoCommand = new AsyncRelayCommand(NuevoRecordatorio);
            EliminarCommand = new AsyncRelayCommand<Recordatorio>(EliminarRecordatorio);
        }

        private async Task NuevoRecordatorio()
        {
            var nuevo = new Recordatorio { Texto = "Nuevo recordatorio" };
            TodosLosRecordatorios.Insert(0, nuevo);
            GuardarCambios();
            await Shell.Current.DisplayAlert("Recordatorio", "Nuevo recordatorio creado", "OK");
        }

        private async Task EliminarRecordatorio(Recordatorio r)
        {
            if (r != null)
            {
                TodosLosRecordatorios.Remove(r);
                GuardarCambios();
                await Shell.Current.DisplayAlert("Eliminado", "Recordatorio eliminado", "OK");
            }
        }

        private void GuardarCambios()
        {
            Recordatorio.SaveAll(TodosLosRecordatorios.ToList());
        }
    }
}
