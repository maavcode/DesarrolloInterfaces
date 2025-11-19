using di.examen._1EV._2025.Backend.Modelos;
using di.examen._1EV._2025.Backend.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

namespace di.examen._1EV._2025.Frontend.Dialogos
{
    /// <summary>
    /// Interaction logic for NuevoEmpleado.xaml
    /// </summary>
    public partial class NuevoEmpleado : Window
    {
        private JardineriaContext _context; // SIEMPRE NECESARIO, RECOGE TODAS LAS TABLAS
        private EmpleadoRepository _empleadoRepository; // NECESARIO YA QUE VAMOS A TOCAR EL EMPLEADO
        private OficinaRepository _oficinaRepository;

        private readonly ILoggerFactory _loggerFactory;
        public NuevoEmpleado()
        {
            InitializeComponent();
        }

        // BOTONES DE VENTANA IMPLEMENTADOS
        private void Btn_Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Minimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Btn_Maximizar_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private async void diagNuevoEmpleado_Loaded(object sender, RoutedEventArgs e)
        {
            _context = new JardineriaContext();// INSTANCIA TODAS LAS TABLAS
            _empleadoRepository = new EmpleadoRepository(_context); // INSTANCIA EL MODELO, PIDE EL CONTEXTO (PARA VER LA TABLA) Y UN NULO
            _oficinaRepository = new OficinaRepository(_context);                                                                                                      

            List<Empleado> jefes = (List<Empleado>)await _empleadoRepository.GetAllAsync(); // CARGAMOS LOS TIPOS DE ARTICULO EN UNA LISTA QUE ESTARA EN EL COMBOBOX
            cmbJefe.ItemsSource = jefes; // CARGAMOS LA LISTA DE TIPOS EN EL COMBO BOX
            
            List<Oficina> oficinas = (List<Oficina>)await _oficinaRepository.GetAllAsync(); // CARGAMOS LOS TIPOS DE ARTICULO EN UNA LISTA QUE ESTARA EN EL COMBOBOX
            cmbOficina.ItemsSource = oficinas; // CARGAMOS LA LISTA DE TIPOS EN EL COMBO BOX
        }

        private void RecogeDatos(Empleado nuevoEmpleado) // RECOGE LOS DATOS INSERTADOS EN EL DIALOGO
        {
            nuevoEmpleado.Nombre = txtNombre.Text;
            nuevoEmpleado.Apellido1 = txtApll1.Text;
            nuevoEmpleado.Apellido2 = txtApll2.Text;
            nuevoEmpleado.Email = txtCorreo.Text;

            nuevoEmpleado.Extension = txtExtension.Text;
            nuevoEmpleado.Puesto = txtPuesto.Text;
            if (cmbJefe.SelectedItem is Empleado empleado)
                nuevoEmpleado.CodigoJefeNavigation = empleado;
            if (cmbOficina.SelectedItem is Oficina oficina)
                nuevoEmpleado.CodigoOficinaNavigation = oficina;

        }

        private async void BtnGuardarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            Empleado empleado = new Empleado();
            RecogeDatos(empleado);

            try
            {
                empleado.CodigoEmpleado = ObtenerSiguienteCodigo(); // ASIGNAR ID DE ARTICULO
                await _empleadoRepository.AddAsync(empleado);
                _context.SaveChanges();
                MessageBox.Show("Todo a ido bien, Empleado " + empleado.Nombre + " ha sido añadido con exito a la base de datos.");
                DialogResult = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private int ObtenerSiguienteCodigo()
        {
            // Obtener el máximo ID actual y sumar 1
            var maxId = _context.Empleados.Max(a => (int?)a.CodigoEmpleado) ?? 0;
            return maxId + 1;
        }

        private void BtnCancelarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
