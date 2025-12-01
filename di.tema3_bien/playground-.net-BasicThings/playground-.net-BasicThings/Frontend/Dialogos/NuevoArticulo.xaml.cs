using Castle.Core.Logging;
using MahApps.Metro.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace playground_.net_BasicThings.Frontend.Dialogos
{
    /// <summary>
    /// Lógica de interacción para NuevoArticulo.xaml
    /// </summary>
    public partial class NuevoArticulo : MetroWindow
    {
        private DiinventarioexamenContext _diinventarrioContext;

        private ArticuloRepository _articuloRepository;
        private ModeloArticuloRepository _modeloArticuloRepository;
        private UsuarioRepository _usuarioRepository;
        private DepartamentoRepository _departamentoRepository;
        private EspacioRepository _espacioRepository;

        // Factory para crear loggers
        private readonly ILoggerFactory _loggerFactory;

        public NuevoArticulo(
            DiinventarioexamenContext diinventarioexamenContext,
            UsuarioRepository usuarioRepository,
            ArticuloRepository articuloRepository, 
            ModeloArticuloRepository modeloArticuloRepository, 
            DepartamentoRepository departamentoRepository,
            EspacioRepository espacioRepository,
            ILoggerFactory loggerFactory)
        {
            InitializeComponent();

            _diinventarrioContext = diinventarioexamenContext;
            _usuarioRepository = usuarioRepository;
            _articuloRepository = articuloRepository;
            _modeloArticuloRepository = modeloArticuloRepository;
            _departamentoRepository = departamentoRepository;
            _espacioRepository = espacioRepository; // note: use existing field name
            _loggerFactory = loggerFactory;
        }

        private async void DiagArticulo_Loaded(object sender, RoutedEventArgs e)
        {

            // Cargar combos con datos de la base
            cmbModelo.ItemsSource = await _modeloArticuloRepository.GetAllAsync();
            cmbUsuario.ItemsSource = await _usuarioRepository.GetAllAsync();
            cmbDepartamento.ItemsSource = await _departamentoRepository.GetAllAsync();
            cmbEspacio.ItemsSource = await _espacioRepository.GetAllAsync();

            // Estado: valores fijos o tabla auxiliar
            cmbEstado.ItemsSource = new List<string> { "Nuevo", "Usado", "Dañado" };
        }

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
        private async void BtnGuardarArticulo_Click(object sender, RoutedEventArgs e)
        {
            Articulo articulo = new Articulo();
            RecogeDatos(articulo);
            
            try
            {
                articulo.Idarticulo = ObtenerSiguienteId(); // ASIGNAR ID DE ARTICULO
                await _articuloRepository.AddAsync(articulo);
                _diinventarrioContext.SaveChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

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
