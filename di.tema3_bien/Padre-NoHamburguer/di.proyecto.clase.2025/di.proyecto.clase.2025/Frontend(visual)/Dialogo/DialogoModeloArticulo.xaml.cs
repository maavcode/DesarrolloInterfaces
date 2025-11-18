using di.proyecto.clase._2025.Backend.Modelos;
using di.proyecto.clase._2025.Backend.Servicios;
using di.proyecto.clase._2025.Backend.Servicios_Repositorio_;
using MahApps.Metro.Controls;
using Mysqlx;
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
    /// Interaction logic for DialogoModeloArticulo.xaml
    /// </summary>
    public partial class DialogoModeloArticulo : MetroWindow
    {
        private DiinventarioexamenContext _context;
        private ModeloArticuloRepository _modeloArticuloRepository;
        private TipoArticuloRepository _tipoArticuloRepository;

        public DialogoModeloArticulo()
        {
            InitializeComponent();
            

        }

        private async void diagModeloArticulo_Loaded(object sender, RoutedEventArgs e)
        {
            _context = new DiinventarioexamenContext();//añadimos
            _modeloArticuloRepository = new ModeloArticuloRepository(_context, null);
            _tipoArticuloRepository = new TipoArticuloRepository(_context, null);
            //se modificará mas adelante
            //Cargamos los tipos de articulo en el combo
            List<Tipoarticulo> tipos = await _tipoArticuloRepository.GetAllAsync();
            cmbTipoArticulo.ItemsSource = tipos;
        }



        private void RecogeDatos(Modeloarticulo modeloarticulo)
        {
            modeloarticulo.Nombre = txtNombre.Text;
            modeloarticulo.Descripcion = txtDescripcion.Text;
            modeloarticulo.Marca = txtMarca.Text;
            modeloarticulo.Modelo = txtModelo.Text;
            if (cmbTipoArticulo.SelectedItem != null)
            {
                modeloarticulo.TipoNavigation = (Tipoarticulo)cmbTipoArticulo.SelectedItem;
            }

        }



        //Botones por activar
        private async void btnAnyadirModeloArticulo_Click(object sender, RoutedEventArgs e)
        {
            Modeloarticulo modeloarticulo = new Modeloarticulo();
            RecogeDatos(modeloarticulo);
            try
            {
                await _modeloArticuloRepository.AddAsync(modeloarticulo);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar " + ex.Message, "Error", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        

        private void btnCancelarModeloArticulo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        
    }
}
