using playground_.net_BasicThings.Backend.Modelos;
using playground_.net_BasicThings.Backend.Servicios;
using playground_.net_BasicThings.Frontend.Mensajes;
using playground_.net_BasicThings.MVVM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace playground_.net_BasicThings.MVVM
{
    // Aquí añades las propiedades y métodos específicos para el ViewModel de Artículo
    public class MVArticuloJero : MVBase
    {
        #region Campos y propiedades privadas
        /// <summary>
        /// Objeto que guarda el modelo de artículo actual
        /// Está vinculado a la vista para mostrar y editar los datos del artículo
        /// </summary>
        private Modeloarticulo _modeloArticulo;
        private Articulo _articulo;
        /// <summary>
        /// Repositorio para gestionar las operaciones de datos relacionadas con los modelos de artículo
        /// </summary>
        private ModeloArticuloRepository _modeloArticuloRepository;
        /// <summary>
        /// Repositorio para gestionar las operaciones de datos relacionadas con los tipos de artículo
        /// </summary>
        private TipoArticuloRepository _tipoArticuloRepository;
        private EspacioRepository _espacioRepository;
        private ArticuloRepository _articuloRepository;
        private DepartamentoRepository _departamentoRepository;

        private UsuarioRepository _usuarioRepository;
        /// <summary>
        /// lista de tipos de artículos disponibles
        /// </summary>
        private List<Tipoarticulo> _listaTipoArticulos;
        private List<Modeloarticulo> _listaModelosArticulos;
        private List<Articulo> _listaArticulos;
        private List<Espacio> _listaEspacios;
        private List<Departamento> _listaDepartamentos;
        private List<Usuario> _listaUsuarios;
        #endregion
        #region Getters y Setters
        public List<Tipoarticulo> listaTiposArticulos => _listaTipoArticulos;
        public List<Modeloarticulo> listaModelosArticulos => _listaModelosArticulos;
        public List<Articulo> listaArticulos => _listaArticulos;
        public List<Espacio> listaEspacios => _listaEspacios;
        public List<Departamento> listaDepartamentos => _listaDepartamentos;
        public List<Usuario> listaUsuarios => _listaUsuarios;

        //"modeloArticulo" será el nombre que pongamos en el binding para que se guarden los datos
        public Modeloarticulo modeloArticulo
        {
            get => _modeloArticulo;
            set => SetProperty(ref _modeloArticulo, value);
        }
        public Articulo articulo
        {
            get => _articulo;
            set => SetProperty(ref _articulo, value);
        }
        #endregion
        // Aquí puedes añadir propiedades y métodos específicos para el ViewModel de Artículo
        public MVArticuloJero(ModeloArticuloRepository modeloArticuloRepository,
                          TipoArticuloRepository tipoArticuloRepository,
                          ArticuloRepository articuloRepository,
                          EspacioRepository espacioRepository,
                          DepartamentoRepository departamentoRepository,
                          UsuarioRepository usuarioRepository)
        {
            _modeloArticuloRepository = modeloArticuloRepository;
            _tipoArticuloRepository = tipoArticuloRepository;
            _articuloRepository = articuloRepository;
            _espacioRepository = espacioRepository;
            _departamentoRepository = departamentoRepository;
            _usuarioRepository = usuarioRepository;
            _modeloArticulo = new Modeloarticulo();
            _articulo = new Articulo();
        }

        public async Task Inicializa()
        {
            try
            {
                _listaTipoArticulos = await GetAllAsync<Tipoarticulo>(_tipoArticuloRepository);
                _listaDepartamentos = await GetAllAsync<Departamento>(_departamentoRepository);
                _listaEspacios = await GetAllAsync<Espacio>(_espacioRepository);
                _listaModelosArticulos = await GetAllAsync<Modeloarticulo>(_modeloArticuloRepository);
                _listaUsuarios = await GetAllAsync<Usuario>(_usuarioRepository);
                _articulo.Fechaalta = DateTime.Now;
            }
            catch (Exception ex)
            {
                MensajeError.Mostrar("GESTIÓN ARTÍCULOS", "Error al cargar los tipos de artículos\n" +
                    "No puedo conectar con la base de datos", 0);
            }
        }

        public async Task<bool> GuardarModeloArticuloAsync()
        {
            bool correcto = true;
            try
            {
                if (modeloArticulo.Idmodeloarticulo == 0)
                {
                    // Nuevo modelo de artículo
                    await _modeloArticuloRepository.AddAsync(modeloArticulo);
                }
                else
                {
                    // Actualizar modelo de artículo existente
                    await _modeloArticuloRepository.UpdateAsync(modeloArticulo);
                }
            }
            catch (Exception ex)
            {
                // Capturamos la excepción y la registramos en el log
                correcto = false;
            }
            return correcto;
        }

        private async Task<int> ObtenerNuevoIdArticulo()
        {
            try
            {

                List<Articulo> articulos = (List<Articulo>)await _articuloRepository.GetAllAsync();


                int maxCodigo = articulos.Max(e => (int?)e.Idarticulo) ?? 0;

                return maxCodigo + 1;
            }
            catch
            {

                return 1000;
            }
        }

        private async void btn_Guardar_Click(object sender, RoutedEventArgs e)
        {
            Articulo articulo = new Articulo();

            articulo.Idarticulo = await ObtenerNuevoIdArticulo();
            RecogeDatos(articulo);

            if (string.IsNullOrEmpty(articulo.Numserie) || string.IsNullOrEmpty(articulo.Estado) || articulo.ModeloNavigation == null)
            {
                MessageBox.Show("Los campos obligatorios no pueden estar vacíos.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                await _articuloRepository.AddAsync(articulo);
                //await _context.SaveChangesAsync();
                MessageBox.Show("Empleado guardado correctamente", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // Método privado para recoger los datos del ViewModel y asignarlos al objeto Articulo.
        // Este método debe rellenar las propiedades del objeto Articulo a partir de los datos del ViewModel.
        // Puedes ajustar los campos según los datos que manejes en tu formulario.
        private void RecogeDatos(Articulo articulo)
        {
            articulo.Numserie = _articulo.Numserie;
            articulo.Estado = _articulo.Estado;
            articulo.Fechaalta = _articulo.Fechaalta;
            articulo.Fechabaja = _articulo.Fechabaja;
            articulo.Usuarioalta = _articulo.Usuarioalta;
            articulo.Usuariobaja = _articulo.Usuariobaja;
            articulo.Modelo = _articulo.Modelo;
            articulo.Departamento = _articulo.Departamento;
            articulo.Espacio = _articulo.Espacio;
            articulo.Dentrode = _articulo.Dentrode;
            articulo.Observaciones = _articulo.Observaciones;
            articulo.ModeloNavigation = _articulo.ModeloNavigation;
            articulo.DepartamentoNavigation = _articulo.DepartamentoNavigation;
            articulo.EspacioNavigation = _articulo.EspacioNavigation;
            articulo.DentrodeNavigation = _articulo.DentrodeNavigation;
            articulo.UsuarioaltaNavigation = _articulo.UsuarioaltaNavigation;
            articulo.UsuariobajaNavigation = _articulo.UsuariobajaNavigation;
        }


        public async Task<bool> GuardarArticuloAsync()
        {
            bool correcto = true;
            try
            {
                if (_articulo.Idarticulo == 0)
                {
                    // Nuevo modelo de artículo
                    await _articuloRepository.AddAsync(articulo);
                }
                else
                {
                    // Actualizar modelo de artículo existente
                    await _articuloRepository.UpdateAsync(articulo);
                }
            }
            catch (Exception ex)
            {
                // Capturamos la excepción y la registramos en el log
                correcto = false;
            }
            return correcto;
        }
    }
}


