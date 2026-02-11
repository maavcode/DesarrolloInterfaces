using playground_.net_BasicThings.Backend.Modelos;
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
    /// Interaction logic for ArbolEspaciosUserControl.xaml
    /// </summary>
    public partial class ArbolEspaciosUserControl : UserControl
    {
        private MVEspacios _mvEspacios;
        public ArbolEspaciosUserControl(MVEspacios mvEspacios)
        {
            InitializeComponent();
            _mvEspacios = mvEspacios;
        }

        private async void arbolEspacios_Loaded(object sender, RoutedEventArgs e)
        {
            await _mvEspacios.Inicializa();
            DataContext = _mvEspacios;
        }
        // RELLENAR TABLA CON LISTA DE ARTICULOS DE UN ESPACIO
        private void treeEspacios_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (treeEspacios.SelectedItem is Espacio)
            {
                dgArticulosPorEspacio.ItemsSource = ((Espacio)treeEspacios.SelectedItem).Articulos;

            }
        }
    }
}
