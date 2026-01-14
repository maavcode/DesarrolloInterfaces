using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using playground_.net_BasicThings.Backend.Modelos;
using playground_.net_BasicThings.Backend.Servicios;
using playground_.net_BasicThings.Frontend.Dialogos;
using playground_.net_BasicThings.Frontend.UserControls;
using playground_.net_BasicThings.MVVM;
using System.Configuration;
using System.Data;
using System.Windows;

namespace playground_.net_BasicThings
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private DiinventarioexamenContext _diinventarioexamenContext;
        /// Propiedad para almacenar el proveedor de servicios
        private IServiceProvider _serviceProvider;
        /// <summary>
        /// Constructor de la clase App
        /// </summary>
        public App()
        {
            // Configurar el contenedor de inyección de dependencias
            var serviceCollection = new ServiceCollection();
            // Configurar los servicios
            ConfigureServices(serviceCollection);
            // Construir el proveedor de servicios
            _serviceProvider = serviceCollection.BuildServiceProvider();
            _diinventarioexamenContext = new DiinventarioexamenContext();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            // Configurar el contexto de la base de datos
            services.AddDbContext<DiinventarioexamenContext>();
            // Configurar el servicio de logging
            services.AddLogging(configure => configure.AddConsole());
            // Registrar repositorios genéricos
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // Registrar servicios y vistas aquí
            // En primer lugar registramos la ventana principal
            services.AddSingleton<MainWindow>();
            // A continuación, registramos los repositorios específicos
            // Lo hacemos con AddScoped para que se cree una nueva instancia
            // de cada repositorio por cada petición
            // Esto es útil para evitar problemas de concurrencia
            services.AddScoped<IGenericRepository<Tipoarticulo>, TipoArticuloRepository>();
            services.AddScoped<IGenericRepository<Modeloarticulo>, ModeloArticuloRepository>();
            services.AddScoped<IGenericRepository<Articulo>, ArticuloRepository>();
            services.AddScoped<IGenericRepository<Usuario>, UsuarioRepository>();
            // Registramos los servicios específicos
            services.AddScoped<UsuarioRepository>();
            services.AddScoped<ArticuloRepository>();
            services.AddScoped<ModeloArticuloRepository>();
            services.AddScoped<TipoArticuloRepository>();
            services.AddScoped<DepartamentoRepository>();
            services.AddScoped<EspacioRepository>();
            // Registramos las interfaces de usuario
            services.AddTransient<Login>();

            services.AddTransient<NuevoModeloArticuloUserControl>();
            services.AddTransient<NuevoModeloArticulo>();

            services.AddTransient<NuevoArticuloUserControl>();
            services.AddTransient<NuevoArticulo>();

            services.AddTransient<ListaModelosArticuloUserControl>();

            services.AddTransient<MVArticuloJero>();
            services.AddTransient<MVModeloArticulo>();
            services.AddTransient<MVArticulo>();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Se genera la ventana de Login
            var loginWindow = _serviceProvider.GetService<Login>();
            loginWindow.ShowDialog();
            base.OnStartup(e);
        }
    }
}
