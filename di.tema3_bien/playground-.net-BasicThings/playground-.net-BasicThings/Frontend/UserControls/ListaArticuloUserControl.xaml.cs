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
    /// Interaction logic for ListaArticuloUserControl.xaml
    /// </summary>
    public partial class ListaArticuloUserControl : UserControl
    {
        private MVArticulo _mvArticulo;
        public ListaArticuloUserControl(
            MVArticulo mvArticulo
            )
        {
            InitializeComponent();
            _mvArticulo = mvArticulo;
        }

        private async void DiagListaArticulos_Loaded(object sender, RoutedEventArgs e)
        {
            // INICIALIZA EL MVARTICULO (CARGA LOS ARTICULOS)
            await _mvArticulo.Inicializa();
            //Esta línea enlaza la interfaz con el MV | SI NO SE PONE DATACONTEXT NO FUNCIONARÁ EL ITEMSOURC
            DataContext = _mvArticulo;
        }
    }
}
