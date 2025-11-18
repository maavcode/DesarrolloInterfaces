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

namespace pruebaNavegacion
{
    /// <summary>
    /// Interaction logic for UserControlArticulos.xaml
    /// </summary>
    public partial class UserControlArticulos : UserControl
    {
        public UserControlArticulos()
        {
            InitializeComponent();
        }

        private void CrearModeloArticulo_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ModeloArticulos();
            dlg.ShowDialog();
        }

        private void CrearArticulo_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Articulos();
            dlg.ShowDialog();
        }
    }
}
