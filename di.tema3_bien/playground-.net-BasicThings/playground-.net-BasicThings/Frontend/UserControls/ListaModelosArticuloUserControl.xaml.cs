using playground_.net_BasicThings.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace playground_.net_BasicThings.Frontend.UserControls
{
    /// <summary>
    /// Interaction logic for ListaModelosArticuloUserControl.xaml
    /// </summary>
    public partial class ListaModelosArticuloUserControl : UserControl
    {
        private MVModeloArticulo _mvModeloArticulo;
        public ListaModelosArticuloUserControl(MVModeloArticulo mVModeloArticulo)
        {
            InitializeComponent();
            _mvModeloArticulo = mVModeloArticulo;
        }

        private async void DiagListaModelosArticulos_Loaded(object sender, RoutedEventArgs e)
        {
            // INICIALIZA EL MVMODELOARTICULO (CARGA LOS TIPOS DE ARTICULO)
            await _mvModeloArticulo.Inicializa();
            //Esta línea enlaza la interfaz con el MV | SI NO SE PONE DATACONTEXT NO FUNCIONARÁ EL ITEMSOURC
            DataContext = _mvModeloArticulo;
        }
    }
}
