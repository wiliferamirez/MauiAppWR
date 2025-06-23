using MauiAppWR.Views;

namespace MauiAppWR
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Views.NotePage), typeof(Views.NotePage));
            Routing.RegisterRoute(nameof(RecordatorioPage), typeof(RecordatorioPage));
        }
    }
}
