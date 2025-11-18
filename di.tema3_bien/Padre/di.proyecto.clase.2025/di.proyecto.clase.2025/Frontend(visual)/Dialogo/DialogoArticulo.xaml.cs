using di.proyecto.clase._2025.Backend.Modelos;
using di.proyecto.clase._2025.Backend.Servicios;
using di.proyecto.clase._2025.Backend.Servicios_Repositorio_;
using MahApps.Metro.Controls;
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

namespace di.proyecto.clase._2025.Frontend_visual_.Dialogo
{
    /// <summary>
    /// Interaction logic for DialogoArticulo.xaml
    /// </summary>
    public partial class DialogoArticulo : MetroWindow
    {
        private DiinventarioexamenContext _context;
        private ArticuloRepository _articuloRepository;
        private ModeloArticuloRepository _modeloArticuloRepository;
        private UsuarioRepository _usuarioRepository;
        private DepartamentoRepository _departamentoRepository;
        private EspacioRepository _espacioRepository;
        public DialogoArticulo()
        {
            InitializeComponent();
        }

        private async void diagArticulo_Loaded(object sender, RoutedEventArgs e)
        {
            _context = new DiinventarioexamenContext();
            _articuloRepository = new ArticuloRepository(_context, null);
            _modeloArticuloRepository = new ModeloArticuloRepository(_context, null);
            _usuarioRepository = new UsuarioRepository(_context, null);
            _departamentoRepository = new DepartamentoRepository(_context, null);
            _espacioRepository = new EspacioRepository(_context, null);

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

        private async void btnGuardarArticulo_Click(object sender, RoutedEventArgs e)
        {
            Articulo articulo = new Articulo();
            RecogeDatos(articulo);

            try
            {
                await _articuloRepository.AddAsync(articulo);
                _context.SaveChanges();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnCancelarArticulo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
