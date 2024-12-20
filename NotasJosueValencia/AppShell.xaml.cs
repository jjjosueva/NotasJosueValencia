using Microsoft.Maui.Controls;

namespace NotasJosueValencia
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registrar rutas para la navegación
            Routing.RegisterRoute(nameof(Views.NoteJVPage), typeof(Views.NoteJVPage));
        }
    }
}
