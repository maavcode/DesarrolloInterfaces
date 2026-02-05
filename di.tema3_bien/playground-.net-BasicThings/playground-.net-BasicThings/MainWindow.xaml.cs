using MahApps.Metro.Controls;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using playground_.net_BasicThings.Frontend.UserControls;

namespace playground_.net_BasicThings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private UserControl? _currentControl;
        private NuevoArticuloUserControl _nuevoArticuloUserControl;
        private NuevoModeloArticuloUserControl _nuevoModeloArticuloUserControl;
        private ListaModelosArticuloUserControl _listaModelosArticuloUserControl;
        private ListaArticuloUserControl _listaArticuloUserControl;
        private NuevoUsuarioUserControl _nuevoUsuarioUserControl;
        private ListaUsuariosUserControl _listaUsuariosUserControl;
        public MainWindow(
            NuevoArticuloUserControl nuevoArticuloUserControl,

            NuevoModeloArticuloUserControl nuevoModeloArticuloUserControl,

            ListaModelosArticuloUserControl listaModelosArticulos,

            ListaArticuloUserControl listaArticulosUserControl
,
            NuevoUsuarioUserControl nuevoUsuarioUserControl,

            ListaUsuariosUserControl listaUsuariosUserControl
            )
        {
            InitializeComponent();
            _nuevoArticuloUserControl = nuevoArticuloUserControl;
            _nuevoModeloArticuloUserControl = nuevoModeloArticuloUserControl;
            _listaModelosArticuloUserControl = listaModelosArticulos;
            _listaArticuloUserControl = listaArticulosUserControl;
            _nuevoUsuarioUserControl = nuevoUsuarioUserControl;
            _listaUsuariosUserControl = listaUsuariosUserControl;
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RouteButton(object sender, SelectionChangedEventArgs e)
        {
            // 1. Determinar cuál ListView generó el evento
            var listView = sender as ListView;
            if (listView?.SelectedItem is not ListViewItem selectedItem)
                return;

            // 2. Obtenemos el texto del elemento pulsado
            string selected = selectedItem.Content.ToString();

            // 3. Limpiamos el panel donde cargamos pantallas
            MainContent.Children.Clear();

            // 4. Declaramos el control que vamos a cargar
            _currentControl = selected switch
            {
                "Nuevo Articulo" => _nuevoArticuloUserControl, // Inyectar cada user control y solo ponerlo aqui sin el new
                "Nuevo Modelo Articulo" => _nuevoModeloArticuloUserControl,
                "Lista de Modelos de Articulo" => _listaModelosArticuloUserControl,
                "Lista de Articulos" => _listaArticuloUserControl,
                "Nuevo Usuario" => _nuevoUsuarioUserControl,
                "Lista Usuarios" => _listaUsuariosUserControl,
                // Aqui los nuevos userControl
                _ => null // Luego el HOME
            };

            // 5. Insertamos el nuevo control
            if (_currentControl != null)
                MainContent.Children.Add(_currentControl);
        }
    }
}