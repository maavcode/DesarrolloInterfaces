using di.examen._1EV._2025.Frontend.Dialogos;
using System.Windows;

namespace di.examen._1EV._2025
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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

        private void Btn_NuevoEmpleado_Click(object sender, RoutedEventArgs e)
        {
            var dialogo = new NuevoEmpleado();
            dialogo.ShowDialog();
        }
    }
}