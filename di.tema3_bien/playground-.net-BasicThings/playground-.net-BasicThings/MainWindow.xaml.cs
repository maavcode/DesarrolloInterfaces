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
            // 1. Determinar cuál ListView generó el evento
            var listView = sender as ListView;
            if (listView?.SelectedItem is not ListViewItem selectedItem)
                return;

            // 2. Obtenemos el texto del elemento pulsado
            string selected = selectedItem.Content.ToString();

            // 3. Limpiamos el panel donde cargamos pantallas
            MainContent.Children.Clear();

            // 4. Declaramos el control que vamos a cargar
            UserControl? control = selected switch
            {
                "Nuevo Articulo" => new NuevoArticuloUserControl(),
                "Nuevo Modelo Articulo" => new NuevoModeloArticuloUserControl(),
                // Aqui los nuevos userControl
                _ => null
            };

            // 5. Insertamos el nuevo control
            if (control != null)
                MainContent.Children.Add(control);
        }
    }
}