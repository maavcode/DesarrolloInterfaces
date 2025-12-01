using Microsoft.Extensions.DependencyInjection;
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
    /// Lógica de interacción para NuevoModeloArticuloUserControl.xaml
    /// </summary>
    public partial class NuevoModeloArticuloUserControl : UserControl
    {
        private NuevoModeloArticulo _nuevoModeloArticulo;
        private IServiceProvider _serviceProvider;
        public NuevoModeloArticuloUserControl(NuevoModeloArticulo nuevoModeloArticulo, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _nuevoModeloArticulo = nuevoModeloArticulo;
            _serviceProvider = serviceProvider;
        }

        private void onNuevoModeloArticuloPulsado(object sender, RoutedEventArgs e)
        {
            _nuevoModeloArticulo = _serviceProvider.GetRequiredService<NuevoModeloArticulo>();
            _nuevoModeloArticulo.ShowDialog(); // SHOWDIALOG() HACE QUE EL DIALOGO PRINCIPAL ESTE BLOQUEADO
        }
    }
}
