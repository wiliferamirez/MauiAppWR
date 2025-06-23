using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppWR.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiAppWR.ViewModels
{
    internal class RecordatoriosViewModel : ObservableObject, IQueryAttributable
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
            await Shell.Current.GoToAsync(nameof(Views.RecordatorioPage));
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
        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("guardado"))
            {
                TodosLosRecordatorios.Clear();
                foreach (var r in Recordatorio.LoadAll())
                    TodosLosRecordatorios.Add(r);
            }
        }

    }
}
