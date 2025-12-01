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
using System.Windows.Shapes;

namespace playground_.net_BasicThings.Frontend.Mensajes
{
    /// <summary>
    /// Interaction logic for VentanaMensaje.xaml
    /// </summary>
    public partial class VentanaMensaje : Window
    {
        MensajeVentana _mensajeVentana;
        public VentanaMensaje(MensajeVentana mensajeVentana)
        {
            InitializeComponent();
            _mensajeVentana = mensajeVentana;
        }

        private void ventanaDialogoMensaje_Loaded(object sender, RoutedEventArgs e)
        {
            imgMensaje.Source = _mensajeVentana.Imagen;
            tbMensaje.Text = _mensajeVentana.Cuerpo;
            tbTitulo.Text = _mensajeVentana.Titulo;
            Aceptar.Background = _mensajeVentana.ColorDistintivo;
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
