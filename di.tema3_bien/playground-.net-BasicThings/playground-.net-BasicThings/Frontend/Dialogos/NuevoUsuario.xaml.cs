using MahApps.Metro.Controls;
using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace playground_.net_BasicThings.Frontend.Dialogos
{
    /// <summary>
    /// Interaction logic for NuevoUsuario.xaml
    /// </summary>
    public partial class NuevoUsuario : MetroWindow
    {
        private MVUsuario _mvUsuario;
        public NuevoUsuario(
            MVUsuario mvUsuario
            )
        {
            InitializeComponent();

            _mvUsuario = mvUsuario;
        }

        private async void diagNuevoUsuario_Loaded(object sender, RoutedEventArgs e)
        {
            // INICIALIZA EL MVARTICULO (CARGA LOS TIPOS DE ARTICULO)
            await _mvUsuario.Inicializa();

            // MANEJA LOS ERRORES DE VALIDACION
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(_mvUsuario.OnErrorEvent));


            //Esta línea enlaza la interfaz con el MV | SI NO SE PONE DATACONTEXT NO FUNCIONARÁ EL ITEMSOURC
            DataContext = _mvUsuario;
        }

        private async void BtnAnyadirUsuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // AÑADE EL MODELO DE  ARTICULO NUEVO
                bool guardado = await _mvUsuario.GuardarActualizarUsuarioAsync();

                if (guardado)
                {
                    MessageBox.Show("Modelo de artículo guardado correctamente",
                                    "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Error al guardar el modelo de artículo",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message,
                               "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancelarUsuario_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // CIERRA EL DIALOGO
        }
    }
}
