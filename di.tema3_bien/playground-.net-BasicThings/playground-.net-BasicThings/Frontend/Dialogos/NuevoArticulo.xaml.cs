using Castle.Core.Logging;
using MahApps.Metro.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using playground_.net_BasicThings.Backend.Modelos;
using playground_.net_BasicThings.Backend.Servicios;
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
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace playground_.net_BasicThings.Frontend.Dialogos
{
    /// <summary>
    /// Lógica de interacción para NuevoArticulo.xaml
    /// </summary>
    public partial class NuevoArticulo : MetroWindow
    {
        private readonly ILoggerFactory _loggerFactory;
        // DECLARACION MVARTICULO
        private MVArticulo _mvArticulo;
        public NuevoArticulo(MVArticulo mVArticulo)
        {
            InitializeComponent();
            _mvArticulo = mVArticulo;
        }

        private async void DiagArticulo_Loaded(object sender, RoutedEventArgs e)
        {
            // INICIALIZA EL MVARTICULO (CARGA LOS TIPOS DE ARTICULO)
            await _mvArticulo.Inicializa();
            // MANEJA LOS ERRORES DE VALIDACION
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(_mvArticulo.OnErrorEvent));
            //Esta línea enlaza la interfaz con el MV | SI NO SE PONE DATACONTEXT NO FUNCIONARÁ EL ITEMSOURC
            DataContext = _mvArticulo;
        }
        /* NO HACE FALTA CON EL MVVM PERO LO DEJO COMO EJEMPLO 
        private void RecogeDatos(Articulo articulo)
        {

            articulo.Numserie = SerieTextBox.Text;
            articulo.Observaciones = txtObservaciones.Text;
            articulo.Fechaalta = dateAlta.SelectedDate.GetValueOrDefault(DateTime.Now);

            if (cmbModelo.SelectedItem is Modeloarticulo modelo)
                articulo.ModeloNavigation = modelo;

            if (cmbUsuario.SelectedItem is Usuario usuario)
                articulo.UsuarioaltaNavigation = usuario;

            if (cmbDepartamento.SelectedItem is Departamento depto)
                articulo.DepartamentoNavigation = depto;

            if (cmbEspacio.SelectedItem is Espacio espacio)
                articulo.EspacioNavigation = espacio;

            if (cmbEstado.SelectedItem != null)
                articulo.Estado = cmbEstado.SelectedItem.ToString();
        }
         /*                                                      */
        // BOTON GUARDAR ARTICULO
        private async void BtnGuardarArticulo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // AÑADE EL MODELO DE  ARTICULO NUEVO
                bool guardado = await _mvArticulo.GuardarArticuloAsync();

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
        /*
        // ESTOOOO PARA GENERAR EL SIGUIENTE ID DE ARTICULO
        private int ObtenerSiguienteId()
        {


            // Obtener el máximo ID actual y sumar 1
            var maxId = _diinventarrioContext.Articulos.Max(a => (int?)a.Idarticulo) ?? 0;
            return maxId + 1;
        }

        /*                                                             */
        private void BtnCancelarArticulo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
