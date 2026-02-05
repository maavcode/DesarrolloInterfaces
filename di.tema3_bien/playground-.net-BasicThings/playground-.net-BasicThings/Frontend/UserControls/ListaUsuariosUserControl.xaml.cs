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
    /// Interaction logic for ListaUsuariosUserControl.xaml
    /// </summary>
    public partial class ListaUsuariosUserControl : UserControl
    {
        private MVUsuario _mvUsuario;
        private IServiceProvider _serviceProvider;
        private NuevoUsuario _nuevoUsuario;
        public ListaUsuariosUserControl(
            MVUsuario mVUsuario,
            IServiceProvider serviceProvider
            )
        {
            InitializeComponent();

            _mvUsuario = mVUsuario;
            _serviceProvider = serviceProvider;
        }

        private async void DiagListaModelosArticulos_Loaded(object sender, RoutedEventArgs e)
        {
            // INICIALIZA EL MVMODELOARTICULO (CARGA LOS TIPOS DE ARTICULO)
            await _mvUsuario.Inicializa();
            //Esta línea enlaza la interfaz con el MV | SI NO SE PONE DATACONTEXT NO FUNCIONARÁ EL ITEMSOURC
            DataContext = _mvUsuario;
        }

        private async void editarModeloArticulo_Click(object sender, RoutedEventArgs e)
        {
            // CREA UNA NUEVA INSTANCIA DEL DIÁLOGO PARA CREAR/EDITAR MODELOS DE ARTÍCULO
            _nuevoUsuario = _serviceProvider.GetRequiredService<NuevoUsuario>();
            // INICIALIZA EL DIÁLOGO PASANDOLE EL MODELO DE ARTÍCULO SELECCIONADO
            await _nuevoUsuario.Inicializa(_mvUsuario.usuario);
            // MUESTRA EL DIÁLOGO
            _nuevoUsuario.ShowDialog();

            if (_nuevoUsuario.DialogResult == true)
            {
                _mvUsuario.listaUsuarios.Refresh();
            }

        }

    }
}