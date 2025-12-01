using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace playground_.net_BasicThings.Frontend.Mensajes
{
    /// <summary>
    /// Enumeración para los tipos de mensajes
    /// </summary>
    public enum TipoMensaje
    {
        Informacion,
        Advertencia,
        Error
    }

    /// <summary>
    /// Clase abstracta base para mostrar mensajes personalizados en la aplicación
    /// </summary>
    public abstract class MensajeBase
    {
        /// <summary>
        /// Título del mensaje
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Cuerpo o contenido principal del mensaje
        /// </summary>
        public string Cuerpo { get; set; }

        /// <summary>
        /// Imagen asociada al mensaje
        /// </summary>
        public BitmapImage Imagen { get; set; }

        /// <summary>
        /// Color distintivo según el tipo de mensaje
        /// </summary>
        public SolidColorBrush ColorDistintivo { get; private set; }

        /// <summary>
        /// Tipo de mensaje (Información, Advertencia, Error)
        /// </summary>
        public TipoMensaje Tipo { get; set; }

        /// <summary>
        /// Tiempo en segundos que permanecerá el mensaje antes de cerrarse.
        /// Si es 0, el mensaje no se cerrará automáticamente.
        /// </summary>
        public int TiempoAutoclose { get; set; }

        /// <summary>
        /// Constructor de la clase base
        /// </summary>
        protected MensajeBase(string titulo, string cuerpo, TipoMensaje tipo, int tiempoAutoclose = 0)
        {
            Titulo = titulo;
            Cuerpo = cuerpo;
            Tipo = tipo;
            TiempoAutoclose = tiempoAutoclose;

            ConfigurarColorEIcono();
        }

        /// <summary>
        /// Configura el color distintivo y la imagen según el tipo de mensaje
        /// </summary>
        private void ConfigurarColorEIcono()
        {
            switch (Tipo)
            {
                case TipoMensaje.Informacion:
                    ColorDistintivo = new SolidColorBrush(Color.FromRgb(33, 150, 243)); // Azul
                    Imagen = CargarIcono("info");
                    break;

                case TipoMensaje.Advertencia:
                    ColorDistintivo = new SolidColorBrush(Color.FromRgb(255, 152, 0)); // Naranja
                    Imagen = CargarIcono("warning");
                    break;

                case TipoMensaje.Error:
                    ColorDistintivo = new SolidColorBrush(Color.FromRgb(244, 67, 54)); // Rojo
                    Imagen = CargarIcono("error");
                    break;
            }
        }

        /// <summary>
        /// Método para cargar iconos desde recursos (debe ser implementado según tu estructura)
        /// </summary>
        private BitmapImage CargarIcono(string nombreIcono)
        {
            BitmapImage imagen;
            try
            {
                // Ajusta la ruta según tu estructura de proyecto
                var uri = new Uri($"pack://application:,,,/Recursos/Imagenes/{nombreIcono}.jpg",
                    UriKind.RelativeOrAbsolute);
                imagen = new BitmapImage(uri);
            }
            catch
            {
                // Si no se encuentra la imagen, devuelve null
                imagen = null;
            }
            return imagen;
        }

        /// <summary>
        /// Método abstracto que debe ser implementado para mostrar el mensaje
        /// </summary>
        public abstract void Mostrar();

        /// <summary>
        /// Método abstracto que debe ser implementado para cerrar el mensaje
        /// </summary>
        public abstract void Cerrar();
    }

    /// <summary>
    /// Implementación concreta de un mensaje con ventana emergente
    /// </summary>
    public abstract class MensajeVentana : MensajeBase
    {
        protected VentanaMensaje _ventana;

        protected MensajeVentana(string titulo, string cuerpo, TipoMensaje tipo, int tiempoAutoclose = 0)
            : base(titulo, cuerpo, tipo, tiempoAutoclose)
        {
        }

        protected void MostrarVentana()
        {
            _ventana = new VentanaMensaje(this);
            _ventana.ShowDialog();

            // Si el tiempo de autoclose es mayor a 0, programar el cierre automático
            if (TiempoAutoclose > 0)
            {
                var timer = new System.Windows.Threading.DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(TiempoAutoclose * 1000)
                };
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    Cerrar();
                };
                timer.Start();
            }
        }

        public override void Cerrar()
        {
            _ventana?.Close();
        }
    }

    /// <summary>
    /// Mensaje de información
    /// </summary>
    public class MensajeInformacion : MensajeVentana
    {
        private MensajeInformacion(string titulo, string cuerpo, int tiempoAutoclose = 0)
            : base(titulo, cuerpo, TipoMensaje.Informacion, tiempoAutoclose)
        {
        }

        public static void Mostrar(string titulo, string cuerpo, int tiempoAutoclose = 0)
        {
            var mensaje = new MensajeInformacion(titulo, cuerpo, tiempoAutoclose);
            mensaje.MostrarVentana();
        }

        public override void Mostrar()
        {
            MostrarVentana();
        }
    }

    /// <summary>
    /// Mensaje de advertencia
    /// </summary>
    public class MensajeAdvertencia : MensajeVentana
    {
        private MensajeAdvertencia(string titulo, string cuerpo, int tiempoAutoclose = 0)
            : base(titulo, cuerpo, TipoMensaje.Advertencia, tiempoAutoclose)
        {
        }

        public static void Mostrar(string titulo, string cuerpo, int tiempoAutoclose = 0)
        {
            var mensaje = new MensajeAdvertencia(titulo, cuerpo, tiempoAutoclose);
            mensaje.MostrarVentana();
        }

        public override void Mostrar()
        {
            MostrarVentana();
        }
    }

    /// <summary>
    /// Mensaje de error
    /// </summary>
    public class MensajeError : MensajeVentana
    {
        private MensajeError(string titulo, string cuerpo, int tiempoAutoclose = 0)
            : base(titulo, cuerpo, TipoMensaje.Error, tiempoAutoclose)
        {
        }

        public static void Mostrar(string titulo, string cuerpo, int tiempoAutoclose = 0)
        {
            var mensaje = new MensajeError(titulo, cuerpo, tiempoAutoclose);
            mensaje.MostrarVentana();
        }

        public override void Mostrar()
        {
            MostrarVentana();
        }
    }


    /// <summary>
    /// Implementación concreta de un mensaje tipo notificación (toast)
    /// </summary>
    /*public class MensajeNotificacion : MensajeBase
    {
        private NotificacionControl _notificacion;

        public MensajeNotificacion(string titulo, string cuerpo, TipoMensaje tipo, int tiempoAutoclose = 3000)
            : base(titulo, cuerpo, tipo, tiempoAutoclose)
        {
        }

        public override void Mostrar()
        {
            _notificacion = new NotificacionControl(this);
            // Aquí deberías agregar la notificación a un contenedor en tu MainWindow
            // Por ejemplo: App.Current.MainWindow.NotificacionContainer.Children.Add(_notificacion);

            if (TiempoAutoclose > 0)
            {
                var timer = new System.Windows.Threading.DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(TiempoAutoclose)
                };
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    Cerrar();
                };
                timer.Start();
            }
        }

        public override void Cerrar()
        {
            // Implementar animación de salida y eliminación del contenedor
            _notificacion?.AnimarSalida();
        }
    }

    // Clases auxiliares que necesitarías crear:
    // - VentanaMensaje.xaml: Window que muestre el mensaje
    // - NotificacionControl.xaml: UserControl para notificaciones tipo toast
}

    // EJEMPLO DE USO:
    /*
    // Mensaje de información que se cierra automáticamente en 3 segundos
    var mensajeInfo = new MensajeVentana(
        "Operación exitosa",
        "Los datos se han guardado correctamente.",
        TipoMensaje.Informacion,
        3000
    );
    mensajeInfo.Mostrar();

    // Mensaje de advertencia que no se cierra automáticamente
    var mensajeAdvertencia = new MensajeVentana(
        "Atención",
        "Algunos campos están vacíos. ¿Desea continuar?",
        TipoMensaje.Advertencia,
        0
    );
    mensajeAdvertencia.Mostrar();

    // Mensaje de error con cierre automático en 5 segundos
    var mensajeError = new MensajeNotificacion(
        "Error de conexión",
        "No se pudo conectar con el servidor.",
        TipoMensaje.Error,
        5000
    );
    mensajeError.Mostrar();
    */
}
