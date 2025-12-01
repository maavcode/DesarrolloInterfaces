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

namespace playground_.net_BasicThings.Frontend.Dialogos
{
    /// <summary>
    /// Lógica de interacción para NuevoModeloArticulo.xaml
    /// </summary>
    public partial class NuevoModeloArticulo : MetroWindow
    {
        private DiinventarioexamenContext _diinventarioexamenContext; // SIEMPRE NECESARIO, RECOGE TODAS LAS TABLAS
        private ModeloArticuloRepository _modeloArticuloRepository; // NECESARIO YA QUE VAMOS A TOCAR EL MODELO DE ARTICULO
        private TipoArticuloRepository _tipoArticuloRepository; // NECESARIO PORQUE VAMOS A USAR EL TIPO DE ARTICULO

        private readonly ILoggerFactory _loggerFactory;

        public NuevoModeloArticulo(
            ModeloArticuloRepository modeloArticuloRepository, 
            TipoArticuloRepository tipoArticuloRepository, 
            ILoggerFactory loggerFactory,
            DiinventarioexamenContext diinventarioexamenContext)
        {
            InitializeComponent();
            _diinventarioexamenContext = diinventarioexamenContext;
            _modeloArticuloRepository = modeloArticuloRepository;
            _tipoArticuloRepository = tipoArticuloRepository;
            _loggerFactory = loggerFactory;
            
        }
        
        private async void diagModeloArticulo_Loaded(object sender, RoutedEventArgs e) // CUANDO SE ABRE EL DIALOGO, HACE LO SIGUIENTE:
        {

            List<Tipoarticulo> tipos = await _tipoArticuloRepository.GetAllAsync(); // CARGAMOS LOS TIPOS DE ARTICULO EN UNA LISTA QUE ESTARA EN EL COMBOBOX
            cmbTipoArticulo.ItemsSource = tipos; // CARGAMOS LA LISTA DE TIPOS EN EL COMBO BOX
        }



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



        //Botones por activar
        private async void BtnAnyadirModeloArticulo_Click(object sender, RoutedEventArgs e) // AÑADE UN MODELO DE ARTICULO
        {
            Modeloarticulo modeloarticulo = new Modeloarticulo(); // CREA EL NUEVO MODELO
            RecogeDatos(modeloarticulo); // RELLENA  EL  NUEVO MODELO CON LOS DATOS RECOGIDOS
            try
            {
                await _modeloArticuloRepository.AddAsync(modeloarticulo); // AÑADE EL MODELO DE  ARTICULO NUEVO
                _diinventarioexamenContext.SaveChanges(); // GUARDA LOS CAMBIOS
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void BtnCancelarModeloArticulo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // CIERRA EL DIALOGO
        }


    }
}


