using playground_.net_BasicThings.Backend.Modelos;
using playground_.net_BasicThings.Backend.Servicios;
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
using MahApps.Metro.Controls;
using Microsoft.Extensions.Logging;
using playground_.net_BasicThings.MVVM;

namespace playground_.net_BasicThings.Frontend.Dialogos
{
    /// <summary>
    /// Lógica de interacción para NuevoModeloArticulo.xaml
    /// </summary>
    public partial class NuevoModeloArticulo : MetroWindow
    {   

        private readonly ILoggerFactory _loggerFactory;
        // DECLARACION DEL MVARTICULO
        private MVModeloArticulo _mvModeloArticulo;
        public NuevoModeloArticulo(MVModeloArticulo mvModeloArticulo)
        {
            InitializeComponent();

            _mvModeloArticulo = mvModeloArticulo;

        }
        
        private async void diagModeloArticulo_Loaded(object sender, RoutedEventArgs e) // CUANDO SE ABRE EL DIALOGO, HACE LO SIGUIENTE:
        {
            // INICIALIZA EL MVARTICULO (CARGA LOS TIPOS DE ARTICULO)
            await _mvModeloArticulo.Inicializa();

            // MANEJA LOS ERRORES DE VALIDACION
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(_mvModeloArticulo.OnErrorEvent));


            //Esta línea enlaza la interfaz con el MV | SI NO SE PONE DATACONTEXT NO FUNCIONARÁ EL ITEMSOURC
            DataContext = _mvModeloArticulo;
        }

        /* YA NO ES NECESARIO PORQUE SE USA EL MVARTICULO
        private void RecogeDatos(Modeloarticulo modeloarticulo) // RECOGE LOS DATOS INSERTADOS EN EL DIALOGO
        {
            modeloarticulo.Nombre = txtNombre.Text; // RECOGE EL DATO DE NOMBRE
            modeloarticulo.Descripcion = txtDescripcion.Text; // RECOGE EL DATO DE DESCRIPCION
            modeloarticulo.Marca = txtMarca.Text; // RECOGE EL DATO DE MARCA
            modeloarticulo.Modelo = txtModelo.Text; // RECOGE EL DATO DE MODELO
            if (cmbTipoArticulo.SelectedItem != null)
            {
                modeloarticulo.TipoNavigation = (Tipoarticulo)cmbTipoArticulo.SelectedItem; // RECOGE EL DATO SELECCIONADO DEL COMBOBOX | EN  LOS CMB SIEMPRE SE USA LOS NAVIGATION (TABLAS RELACIONALES)
            }

        }
        */

        //Botones por activar
        private async void BtnAnyadirModeloArticulo_Click(object sender, RoutedEventArgs e) // AÑADE UN MODELO DE ARTICULO
        {
            try
            {
                // AÑADE EL MODELO DE  ARTICULO NUEVO
                bool guardado = await _mvModeloArticulo.GuardarModeloArticuloAsync();

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
            } catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message,
                               "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void BtnCancelarModeloArticulo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // CIERRA EL DIALOGO
        }


    }
}


