using Microsoft.Extensions.Logging;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private DiinventarioexamenContext _diinventarioexamenContext;
        private UsuarioRepository _usuarioRepository;
        private ILogger<GenericRepository<Usuario>> _logger;
        public Login()
        {
            InitializeComponent();
            // Instanciar el contexto y el repositorio
            _diinventarioexamenContext = new DiinventarioexamenContext();
            _usuarioRepository = new UsuarioRepository(_diinventarioexamenContext, null);
        }
        private async void btnLogin_Ckick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(passClave.Password))
            {
                bool isAuthenticated = await _usuarioRepository.LoginAsync(txtUsuario.Text, passClave.Password);
                if (!isAuthenticated)
                {
                    MessageBox.Show("Usuario o clave incorrectos.", "Error de autenticación", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    MainWindow ventanaPrincipal = new MainWindow();
                    ventanaPrincipal.Show();
                    this.Close();
                }

            }
            else
            {
                MessageBox.Show("Por favor, introduzca usuario y clave.", "Error de autenticación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ventanaLogin_Loaded(object sender, RoutedEventArgs e)
        {
            // Instanciar el contexto y el repositorio
            // El contecto nos permite acceder a la base de datos
            _diinventarioexamenContext = new DiinventarioexamenContext();
            
            // Creamos un logger para el repositorio
            _logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<GenericRepository<Usuario>>();
            
            // El repositorio nos permite hacer operaciones CRUD sobre la entidad Usuario
            _usuarioRepository = new UsuarioRepository(_diinventarioexamenContext, _logger);
        }
    }
}
