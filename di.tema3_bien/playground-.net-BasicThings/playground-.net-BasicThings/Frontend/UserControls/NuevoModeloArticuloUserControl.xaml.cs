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
        public NuevoModeloArticuloUserControl()
        {
            InitializeComponent();
        }

        private void onNuevoModeloArticuloPulsado(object sender, RoutedEventArgs e)
        {
            var dialogo = new NuevoModeloArticulo(); // CREO EL DIALOGO
            dialogo.ShowDialog(); // SHOWDIALOG() HACE QUE EL DIALOGO PRINCIPAL ESTE BLOQUEADO
        }
    }
}
