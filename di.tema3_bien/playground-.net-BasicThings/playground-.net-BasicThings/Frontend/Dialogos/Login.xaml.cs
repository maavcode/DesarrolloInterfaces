using Microsoft.Extensions.Logging;
using playground_.net_BasicThings.Backend.Modelos;
using playground_.net_BasicThings.Backend.Servicios;
using playground_.net_BasicThings.Frontend.Mensajes;
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
        private readonly UsuarioRepository _usuarioRepository;
        private MainWindow _ventanaPrincipal;
        public Login(UsuarioRepository usuarioRepository, MainWindow ventanaPrincipal)
        {
            InitializeComponent();
            _usuarioRepository = usuarioRepository;
            _ventanaPrincipal = ventanaPrincipal;
        }

        private async void onBotonLoginPulsado(object sender, RoutedEventArgs e) // Estos parametros existen para hacer saber al programa que existe el evento y que es un evento
        {
            if (!string.IsNullOrEmpty(nombreUsuario.Text) && !string.IsNullOrEmpty(claveUsuario.Password))
            {
                // isAutenthicated realiza una funcion que comprueba si existe el usuario y si su contraseña es correcta
                bool isAuthenticated = await _usuarioRepository.LoginAsync(nombreUsuario.Text, claveUsuario.Password);
                if (isAuthenticated) {
                    // Como el usuario y la clave son correctos:
                    // Enseña el nuevo dialogo
                    _ventanaPrincipal.Show();
                    // Esconde el dialogo Login
                    this.Close();
                }
                else // Si algun campo es incorrecto
                {
                    MensajeAdvertencia.Mostrar("Error de autenticación", "Usuario o clave incorrectos.");
                    return;
                }
            }
            else // Si no has completado los campos
            {
               MensajeError.Mostrar("Error de autentificacion", "Introduce algo");
            } 
        }
    }
}
