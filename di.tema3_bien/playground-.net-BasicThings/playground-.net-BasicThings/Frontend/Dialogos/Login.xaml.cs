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

namespace playground_.net_BasicThings.Frontend.Dialogos
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private DiinventarioexamenContext _diinventarioexamenContext;
        private UsuarioRepository _usuarioRepository;

        // Factory para crear loggers
        // Este objecto sirve para crear loggers
        private readonly ILoggerFactory _loggerFactory;
        public Login()
        {
            // Inicializar LoggerFactory
            _loggerFactory = new LoggerFactory();

            InitializeComponent();
            // Instanciar el contexto y el repositorio
            _diinventarioexamenContext = new DiinventarioexamenContext();

            _usuarioRepository = new UsuarioRepository(
                _diinventarioexamenContext, 
                // El Logger factory, crea un nuevo ILogger basado en GenericRepository<modeloAUsar>
                _loggerFactory.CreateLogger<GenericRepository<Usuario>>()
                );
        }

        private async void onBotonLoginPulsado(object sender, RoutedEventArgs e) // Estos parametros existen para hacer saber al programa que existe el evento y que es un evento
        {
            if (!string.IsNullOrEmpty(nombreUsuario.Text) && !string.IsNullOrEmpty(claveUsuario.Password))
            {
                // isAutenthicated realiza una funcion que comprueba si existe el usuario y si su contraseña es correcta
                bool isAuthenticated = await _usuarioRepository.LoginAsync(nombreUsuario.Text, claveUsuario.Password);
                if (isAuthenticated) {
                    // Como el usuario y la clave son correctos:
                    // Instancia el  nuevo Dialogo, MainWindow
                    MainWindow ventanaPrincipal = new MainWindow();
                    // Enseña el nuevo dialogo
                    ventanaPrincipal.Show();
                    // Cierra el dialogo Login
                    this.Close();
                }
                else // Si algun campo es incorrecto
                {
                    MessageBox.Show("Usuario o clave incorrectos.", "Error de autenticación", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else // Si no has completado los campos
            {
                MessageBox.Show("Por favor, introduzca usuario y clave.", "Error de autenticación", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
        }
    }
}
