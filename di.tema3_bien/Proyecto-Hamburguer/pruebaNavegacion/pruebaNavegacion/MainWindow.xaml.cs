using System.Text;
using System.Windows;
using MahApps.Metro.Controls;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pruebaNavegacion
{

    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void hamMenuPrincipal_ItemClick(object sender, ItemClickEventArgs args)
        {
            // Identificamos el item clicado. Si es un HamburgerMenuItem, obtenemos su Tag
            var hmi = args.ClickedItem as HamburgerMenuItem;
            string etiqueta;
            // Si el Tag no es nulo, lo convertimos a cadena; si es nulo, usamos cadena vacía
            if (hmi?.Tag != null)
            {
                etiqueta = hmi.Tag.ToString() ?? string.Empty;
            }
            else
            {
                etiqueta = string.Empty;
            }

            // Segun la etiqueta, cargamos el UserControl correspondiente
            switch (etiqueta)
            {
                case "Prestamos":
                    // Placeholder: reemplaza por tu UserControl de artículos cuando lo tengas

                case "Busqueda":
                    // Placeholder: reemplaza por tu UserControl de artículos cuando lo tengas
                    hamMenuPrincipal.Content = new UserControlBusqueda();
                    break;
                case "Articulos":
                    hamMenuPrincipal.Content = new UserControlArticulos();
                    break;
                case "Administración":
                    // Placeholder: reemplaza por tu UserControl de artículos cuando lo tengas
                    //hamMenuPrincipal.Content = new UserControlAdministracion();
                    break;
                case "Inventario":
                    // Placeholder: reemplaza por tu UserControl de artículos cuando lo tengas
                    hamMenuPrincipal.Content = new UserControlInventario();
                    break;
                case "Información":
                    // Placeholder: reemplaza por tu UserControl de artículos cuando lo tengas
                    hamMenuPrincipal.Content = new UserControlInformacion();
                    break;

                default:
                    break;
            }
        }

        // Método auxiliar para mostrar un marcador visual mientras creas UserControls reales
        private static UIElement CreatePlaceholder(string texto)
        {
            return new TextBlock
            {
                Text = texto,
                Foreground = Brushes.White,
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
        }
    }
}