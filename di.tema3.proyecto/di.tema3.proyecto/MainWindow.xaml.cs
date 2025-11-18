using di.tema3.proyecto.Frontend.Dialogos;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;
using di.tema3.proyecto.Frontend.ControlUsuario;


namespace di.tema3.proyecto
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RouteButton(object sender, SelectionChangedEventArgs e)
        {
            // Declara una pantalla. PUEDES HACER UNA POR DEFECTO
            UserControl? control = null;
            // Declaro la variable donde sera seleccionado el CONTENIDO del bloque PULSADO
            string? selected = null;

            if (PrestamosListView.SelectedItem is ListViewItem selectedPrestamos)
            {
                selected = selectedPrestamos.Content.ToString(); // Selecciona el CONTENIDO del bloque PULSADO

                // Limpiar el contenido previo del maincontent
                MainContent.Children.Clear();

                switch (selected)
                {
                    case "Salidas": 
                        control = new SalidasControl(); // Crear e insertar el control
                        MainContent.Children.Add(control); // Pone un nuevo texto en maincontent
                        break;
                    case "Entradas":
                        control = new SalidasControl(); // Crear e insertar el control
                        MainContent.Children.Add(control); // Pone un nuevo texto en maincontent
                        break;
                    case "Busqueda":
                        control = new SalidasControl(); // Crear e insertar el control
                        MainContent.Children.Add(control); // Pone un nuevo texto en maincontent
                        break;
                    case "Listado pendientes":
                        control = new SalidasControl(); // Crear e insertar el control
                        MainContent.Children.Add(control); // Pone un nuevo texto en maincontent
                        break;
                }
            } else if (ArticulosListView.SelectedItem is ListViewItem selectedArticulos)
            {
                selected = selectedArticulos.Content.ToString(); // Selecciona el CONTENIDO del bloque PULSADO

                // Limpiar el contenido previo del maincontent
                MainContent.Children.Clear();

                switch (selected)
                {
                    case "Articulos":
                        control = new SalidasControl(); // Crear e insertar el control
                        MainContent.Children.Add(control); // Pone un nuevo texto en maincontent
                        break;
                    case "Modelo Articulo":
                        control = new SalidasControl(); // Crear e insertar el control
                        MainContent.Children.Add(control); // Pone un nuevo texto en maincontent
                        break;
                    case "Tipo de Articulos":
                        control = new SalidasControl(); // Crear e insertar el control
                        MainContent.Children.Add(control); // Pone un nuevo texto en maincontent
                        break;
                }
            }
        }
    }
}
