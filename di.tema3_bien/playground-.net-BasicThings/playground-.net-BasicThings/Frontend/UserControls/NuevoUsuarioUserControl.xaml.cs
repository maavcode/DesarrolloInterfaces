using Microsoft.Extensions.DependencyInjection;
using playground_.net_BasicThings.Backend.Modelos;
using playground_.net_BasicThings.Frontend.Dialogos;
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
    /// Interaction logic for NuevoUsuarioUserControl.xaml
    /// </summary>
    public partial class NuevoUsuarioUserControl : UserControl
    {
        private NuevoUsuario _nuevoUsuario;
        private IServiceProvider _serviceProvider;
        public NuevoUsuarioUserControl(
            NuevoUsuario nuevoUsuario,
            IServiceProvider serviceProvider
            )
        {
            InitializeComponent();
            _nuevoUsuario = nuevoUsuario;
            _serviceProvider = serviceProvider;
        }

        private async void onNuevoUsuarioPulsado(object sender, RoutedEventArgs e)
        {
            _nuevoUsuario = _serviceProvider.GetRequiredService<NuevoUsuario>();
            await _nuevoUsuario.Inicializa(new Usuario());
            _nuevoUsuario.ShowDialog(); // SHOWDIALOG() HACE QUE EL DIALOGO PRINCIPAL ESTE BLOQUEADO
        }
    }
}
