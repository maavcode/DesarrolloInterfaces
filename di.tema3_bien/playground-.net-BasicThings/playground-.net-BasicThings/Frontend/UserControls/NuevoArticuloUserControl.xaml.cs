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
    /// Lógica de interacción para NuevoArticuloUserControl.xaml
    /// </summary>
    public partial class NuevoArticuloUserControl : UserControl
    {
        private NuevoArticulo _nuevoArticulo;
        private IServiceProvider _serviceProvider;
        public NuevoArticuloUserControl(NuevoArticulo nuevoArticulo, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _nuevoArticulo = nuevoArticulo;
            _serviceProvider = serviceProvider;
        }

        private void onNuevoArticuloPulsado(object sender, RoutedEventArgs e)
        {
            _nuevoArticulo = _serviceProvider.GetRequiredService<NuevoArticulo>();
            _nuevoArticulo.Inicializa(new Articulo());
            _nuevoArticulo.ShowDialog(); // SHOWDIALOG() HACE QUE EL DIALOGO PRINCIPAL ESTE BLOQUEADO
        }
    }
}
