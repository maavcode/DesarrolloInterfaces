using di.proyecto.clase._2025.Backend.Modelos;
using di.proyecto.clase._2025.Backend.Servicios;
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
using System.Windows.Shapes;

namespace di.proyecto.clase._2025.Frontend_visual_.Dialogo
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        //Añadimos
        private DiinventarioexamenContext _context;
        private UsuarioRepository _usuarioRepository;
        //private UsuarioRepository _usuarioRepository;
        public Login()
        {
            InitializeComponent();
            _context = new DiinventarioexamenContext();//añadimos
            _usuarioRepository = new UsuarioRepository(_context,null);
            //UsuarioRepository usuarioRepository = new usuarioRepository
        }

        /*
         * el boton de login hará que abra otra ventana del main windows
         * añadimos async para poder validar por la BBDD
         */
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(passClave.Password))
            {
                //Añadimos el accesoPermitido para poder validar el usuario y la contraseña
                //y solo si esta en la base de datos podrá iniciar sesion
                // Validación directa usando los controles txtUsuario y passClave
                bool accesoPermitido = await _usuarioRepository.LoginAsync(txtUsuario.Text, passClave.Password);
                //Tambien añadimos este if
                if (accesoPermitido)
                {
                    //Esta ya estaba
                    MainWindow ventanaPrincipal = new MainWindow();
                    ventanaPrincipal.Show();
                    this.Close();
                }
                //y añadimos el else en el caso de que no este en la bbdd
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error de autenticación",
                                           MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            
            }
            else
            {
                MessageBox.Show("Por favor introduce usuario y clave.", "Error de autenticación",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}
