using Microsoft.Extensions.DependencyInjection;
using playground_.net_BasicThings.Frontend.Dialogos;
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

        private NuevoModeloArticulo _nuevoModeloArticulo;
        private IServiceProvider _serviceProvider;
        public ListaModelosArticuloUserControl(
            MVModeloArticulo mVModeloArticulo,
            IServiceProvider serviceProvider
            )
        {
            InitializeComponent();
            _mvModeloArticulo = mVModeloArticulo;
            _serviceProvider = serviceProvider;
        }

        private async void DiagListaModelosArticulos_Loaded(object sender, RoutedEventArgs e)
        {
            // INICIALIZA EL MVMODELOARTICULO (CARGA LOS TIPOS DE ARTICULO)
            await _mvModeloArticulo.Inicializa();
            //Esta línea enlaza la interfaz con el MV | SI NO SE PONE DATACONTEXT NO FUNCIONARÁ EL ITEMSOURC
            DataContext = _mvModeloArticulo;
        }

        private async void editarModeloArticulo_Click(object sender, RoutedEventArgs e)
        {
            // CREA UNA NUEVA INSTANCIA DEL DIÁLOGO PARA CREAR/EDITAR MODELOS DE ARTÍCULO
            _nuevoModeloArticulo = _serviceProvider.GetRequiredService<NuevoModeloArticulo>();
            // INICIALIZA EL DIÁLOGO PASANDOLE EL MODELO DE ARTÍCULO SELECCIONADO
            await _nuevoModeloArticulo.Inicializa(_mvModeloArticulo.modeloArticulo);
            // MUESTRA EL DIÁLOGO
            _nuevoModeloArticulo.ShowDialog();

            if (_nuevoModeloArticulo.DialogResult == true)
            {
                _mvModeloArticulo.listaModelosArticulo.Refresh();
            }

        }

        private void eliminarModeloArticulo_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
