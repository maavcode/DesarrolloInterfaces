using di.proyecto.clase._2025.Frontend_visual_.Dialogo;
using Fluent;
using MaterialDesignThemes.Wpf;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace di.proyecto.clase._2025
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAddModeloArticulo_Click(object sender, RoutedEventArgs e)
        {
            DialogoModeloArticulo dialogoModeloArticulo = new DialogoModeloArticulo();
            dialogoModeloArticulo.ShowDialog();

        }

        private void btnAddArticulo_Click(object sender, RoutedEventArgs e)
        {
            DialogoArticulo dialogoArticulo = new DialogoArticulo();
            dialogoArticulo.ShowDialog();
        }

    }
}