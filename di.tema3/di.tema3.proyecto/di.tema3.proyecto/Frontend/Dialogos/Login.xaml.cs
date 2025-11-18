using di.tema3.proyecto.Backend.Modelo;
using di.tema3.proyecto.Backend.Servicios;
using Microsoft.Extensions.Logging;
using System.Net.NetworkInformation;
using System.Windows;
using Microsoft.Extensions.Logging.Abstractions;

namespace di.tema3.proyecto.Frontend.Dialogos
{
    using di.tema3.proyecto.Backend.Modelo;
    using di.tema3.proyecto.Backend.Servicios;
    using Microsoft.Extensions.Logging.Abstractions;

    public partial class Login : Window
    {
        private DiinventarioexamenContext _context;
        private ILogger<GenericRepository<Usuario>> _logger;
        private UsuarioRepository _usuarioRepository ;

        public Login()
        {
            InitializeComponent();
        }
    


    private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = TxtUsername.Text.Trim();
            string password = TxtPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, introduce usuario y contraseña.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                bool loginSuccess = await _usuarioRepository.LoginAsync(username, password);

                if (loginSuccess)
                {
                    var main = new MainWindow { WindowState = WindowState.Maximized };
                    main.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ventanaLogin_Loaded(object sender, RoutedEventArgs e)
        {
          
            // context = new DiinventarioexamenContext();
            //

            _context = new DiinventarioexamenContext();
            _logger = NullLogger<GenericRepository<Usuario>>.Instance; // Logger "nulo"
            
            _logger = LoggerFactory.Create(FilterLoggingBuilderExtensions =>
            {
                FilterLoggingBuilderExtensions.AddConsole();
            }).CreateLogger<GenericRepository<Usuario>>();
            _usuarioRepository = new UsuarioRepository(_context, _logger);
        }
    }
    
   
}
