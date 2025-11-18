using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using pruebaNavegacion.Backend.Modelo;
using pruebaNavegacion.Backend.Servicios;
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

namespace pruebaNavegacion
{
    /// <summary>
    /// Interaction logic for Articulos.xaml
    /// </summary>
    public partial class ModeloArticulos : Window
    {
        private DiinventarioexamenContext _diinventarioexamenContext;
        private ModeloArticuloRepository _ModeloArticuloRepository;
        private TipoArticuloRespository _tipoArticuloRespository;
        private ILogger<GenericRepository<Modeloarticulo>> _loggerModeloArticulo;
        private ILogger<GenericRepository<Tipoarticulo>> _loggerTipoArticulo;


        public ModeloArticulos()
        {
            InitializeComponent();
        }

        private async void diagModeloArticulo_Loaded(object sender, RoutedEventArgs e)
        {
            // Instanciamos el contexto
            _diinventarioexamenContext = new DiinventarioexamenContext();

            // Configuramos los loggers
            _loggerModeloArticulo = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<GenericRepository<Modeloarticulo>>();
            _loggerTipoArticulo = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<GenericRepository<Tipoarticulo>>();


            // Instanciamos los repositorios
            _ModeloArticuloRepository = new ModeloArticuloRepository(_diinventarioexamenContext, _loggerModeloArticulo);
            _tipoArticuloRespository = new TipoArticuloRespository(_diinventarioexamenContext, _loggerTipoArticulo);

            // Cargamos los tipos de articulo en el ComboBox
            List<Tipoarticulo> tiposArticulo = await _tipoArticuloRespository.GetAllAsync();
            cmbTipoArticulo.ItemsSource = tiposArticulo;
        }

        private void btnModeloArticulo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void RecogeDatos(Modeloarticulo modeloarticulo)
        {
            modeloarticulo.Nombre = txtNombre.Text;
            modeloarticulo.Descripcion = txtDescripcion.Text;
            modeloarticulo.Marca = txtMarca.Text;
            modeloarticulo.Modelo = txtModelo.Text;
            if(cmbTipoArticulo.SelectedItem != null)
            {
                modeloarticulo.TipoNavigation = (Tipoarticulo)cmbTipoArticulo.SelectedItem;
            }
        }
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Modeloarticulo modeloarticulo = new Modeloarticulo();
                RecogeDatos(modeloarticulo);
                await _ModeloArticuloRepository.AddAsync(modeloarticulo);
                _diinventarioexamenContext.SaveChanges();
                MessageBox.Show("Modelo de artículo guardado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el modelo de artículo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}