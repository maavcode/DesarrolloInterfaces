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
        private DiinventarioexamenContext _context; // SIEMPRE NECESARIO, RECOGE TODAS LAS TABLAS
        private ModeloArticuloRepository _modeloArticuloRepository; // NECESARIO YA QUE VAMOS A TOCAR EL MODELO DE ARTICULO
        private TipoArticuloRepository _tipoArticuloRepository; // NECESARIO PORQUE VAMOS A USAR EL TIPO DE ARTICULO

        public DialogoModeloArticulo()
        {
            InitializeComponent();
            

        }

        private async void diagModeloArticulo_Loaded(object sender, RoutedEventArgs e) // CUANDO SE ABRE EL DIALOGO, HACE LO SIGUIENTE:
        {
            _context = new DiinventarioexamenContext();// INSTANCIA TODAS LAS TABLAS
            _modeloArticuloRepository = new ModeloArticuloRepository(_context, null); // INSTANCIA EL MODELO, PIDE EL CONTEXTO (PARA VER LA TABLA) Y UN NULO
            _tipoArticuloRepository = new TipoArticuloRepository(_context, null); // INSTANCIA EL TIPO ARTICULO, PIDE EL CONTEXTO (PARA VER LA TABLA) Y UN NULO
            //se modificará mas adelante
            
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
        private async void btnAnyadirModeloArticulo_Click(object sender, RoutedEventArgs e) // AÑADE UN MODELO DE ARTICULO
        {
            Modeloarticulo modeloarticulo = new Modeloarticulo(); // CREA EL NUEVO MODELO
            RecogeDatos(modeloarticulo); // RELLENA  EL  NUEVO MODELO CON LOS DATOS RECOGIDOS
            try
            {
                await _modeloArticuloRepository.AddAsync(modeloarticulo); // AÑADE EL MODELO DE  ARTICULO NUEVO
                _context.SaveChanges(); // GUARDA LOS CAMBIOS
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar " + ex.Message, "Error", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        

        private void btnCancelarModeloArticulo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // CIERRA EL DIALOGO
        }

        
    }
}
