using Microsoft.EntityFrameworkCore.Query;
using playground_.net_BasicThings.Backend.Modelos;
using playground_.net_BasicThings.Backend.Servicios;
using playground_.net_BasicThings.Frontend.Mensajes;
using playground_.net_BasicThings.MVVM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace playground_.net_BasicThings.MVVM
{
    public class MVArticulo : MVBase
    {
        #region Campos y propiedades privadas
        // Propiedad que guarda el artículo actual y propiedad que guarda el repositorio del mismo
        private Articulo _articulo;
        private ArticuloRepository _articuloRepository;
        // Propiedad que guarda el espacio actual y propiedad que guarda el repositorio del mismo
        private Espacio _espacio;
        private EspacioRepository _espacioRepository;
        // Propiedad que guarda el departamento actual y propiedad que guarda el repositorio del mismo
        private Departamento _departamento;
        private DepartamentoRepository _departamentoRepository;
        // Propiedad que guarda el usuario actual y propiedad que guarda el repositorio del mismo
        private Usuario _usuario;
        private UsuarioRepository _usuarioRepository;
        // Propiedad que guarda el modelo de artículo actual y propiedad que guarda el repositorio del mismo
        private Tipoarticulo _tipoarticulo;
        private TipoArticuloRepository _tipoArticuloRepository;
        // Propiedad que guarda el modelo de artículo actual y propiedad que guarda el repositorio del mismo
        private Modeloarticulo _modeloarticulo;
        private ModeloArticuloRepository _modeloArticuloRepository;

        // Listas de datos para los combo box
        private List<Tipoarticulo> _listaTipoArticulos;
        private List<Modeloarticulo> _listaModeloArticulos;
        private List<String> _listaEstados;
        private List<Articulo> _listaArticulos;
        private List<Espacio> _listaEspacios;
        private List<Departamento> _listaDepartamentos; 
        private List<Usuario> _listaUsuarios;
        #endregion
        #region Filtros

        #region Si el filtro se activa por boton
        // Estos estan porque el filtro se activa por boton
        private int _totalResultados;

        public int totalResultados
        {
            get => _totalResultados;
            set => SetProperty(ref _totalResultados, value);
        }
        private int _contadorResultados;
        #endregion

        // Predicado general para aplicar los filtros a la lista de artículos
        private Predicate<object> _predicadoFiltros;
        // Lista de criterios para filtrar los artículos
        private List<Predicate<Articulo>> _criterios;

        // Propiedad para ver el número de resultados que se podran mostrar
        private int _maximoResultados;
        public int maximoResultados
        {
            get => _maximoResultados;
            set => SetProperty(ref _maximoResultados, value);
        }

        #region Filtro por fecha inicio y final alta
        // Filtro por fecha de alta
        private Predicate<Articulo> _criterioFecha;
        // Fecha inicial del filtro
        private DateTime _fechaInicial;

        public DateTime fechaInicial
        {
            get => _fechaInicial;
            set => SetProperty(ref _fechaInicial, value);
        }

        // Fecha final del filtro
        private DateTime _fechaFinal;

        public DateTime fechaFinal
        {
            get => _fechaFinal;
            set => SetProperty(ref _fechaFinal, value);
        }
        #endregion

        #region Filtro por Numero de serie
        private Predicate<Articulo> _criterioNumeroSerie;
        private string _textoNumeroSerie;

        public string textoNumeroSerie
        {
            get => _textoNumeroSerie;
            set => SetProperty(ref _textoNumeroSerie, value);
        }
        #endregion

        #region Filtro por Espacio
        private Predicate<Articulo> _criterioEspacio;

        private Espacio _espacioSeleccionado;

        public Espacio espacioSeleccionado
        {
            get => _espacioSeleccionado;
            set => SetProperty(ref _espacioSeleccionado, value);
        }
        #endregion

        #region Filtro por Resultados
        // Se usa el total de resultados y maximo
        #endregion

        #endregion
        #region Getters y Setters
        // Funcion getter y setter para el artículo actual
        public Articulo articulo
        {
            get => _articulo;
            set => SetProperty(ref _articulo, value);
        }
        // Funciones getter y setter para los combo box
        public List<Tipoarticulo> listaTiposArticulos => _listaTipoArticulos;
        public List<Modeloarticulo> listaModelosArticulos => _listaModeloArticulos;
        public List<String> listaEstados => _listaEstados;
        public ListCollectionView listaArticulos { get; set; }
        public List<Espacio> listaEspacios => _listaEspacios;
        public List<Departamento> listaDepartamentos => _listaDepartamentos;
        public List<Usuario> listaUsuarios => _listaUsuarios;

        #endregion
        // Constructor que recibe el artículo y el repositorio como parámetros
        public MVArticulo(
            ArticuloRepository articuloRepository,
            EspacioRepository espacioRepository,
            DepartamentoRepository departamentoRepository,
            UsuarioRepository usuarioRepository,
            TipoArticuloRepository tipoArticuloRepository,
            ModeloArticuloRepository modeloArticuloRepository
            )
        {
            _articuloRepository = articuloRepository;
            _espacioRepository = espacioRepository;
            _departamentoRepository = departamentoRepository;
            _usuarioRepository = usuarioRepository;
            _tipoArticuloRepository = tipoArticuloRepository;
            _modeloArticuloRepository = modeloArticuloRepository;
        }
        #region Metodos privados
        public async Task InicializaListas()
        {
            _listaEstados =
                   new List<string> { "Nuevo", "Usado", "Reparación", "Baja" };
            OnPropertyChanged(nameof(listaEstados));

            _listaUsuarios =
                await GetAllAsync<Usuario>(_usuarioRepository);
            OnPropertyChanged(nameof(listaUsuarios));

            _listaModeloArticulos =
                await GetAllAsync<Modeloarticulo>(_modeloArticuloRepository);
            OnPropertyChanged(nameof(listaModelosArticulos));

            _listaDepartamentos =
                await GetAllAsync<Departamento>(_departamentoRepository);
            OnPropertyChanged(nameof(listaDepartamentos));

            _listaEspacios =
                await GetAllAsync<Espacio>(_espacioRepository);
            OnPropertyChanged(nameof(listaEspacios));
            // 
            _listaArticulos =
                   await GetAllAsync<Articulo>(_articuloRepository);
            OnPropertyChanged(nameof(listaArticulos));
            listaArticulos = new ListCollectionView(_listaArticulos);
            // Establecer el número máximo de resultados al tamaño de la lista de artículos
            maximoResultados = _listaArticulos.Count;
            // Inicializar el número total de resultados al máximo permitido
            totalResultados = maximoResultados;

            // Inicializar la lista de criterios de filtro
            _criterios = new List<Predicate<Articulo>>();
            
            //  
            fechaInicial = DateTime.UtcNow.AddMonths(-1);
            fechaFinal = DateTime.UtcNow;
        }

        #region Metodos Filtros
        private void InicializaCriterios()
        {
            // Inicializa el criterio de filtro por fecha de alta
            _criterioFecha = new Predicate<Articulo>(a =>
                                    a.Fechaalta >= _fechaInicial &&
                                    a.Fechaalta <= _fechaFinal
                                );


            _criterioNumeroSerie = new Predicate<Articulo>(a =>
                                    !string.IsNullOrEmpty(_textoNumeroSerie) &&
                                    !string.IsNullOrEmpty(a.Numserie) &&
                                    a.Numserie.StartsWith(_textoNumeroSerie, StringComparison.OrdinalIgnoreCase)
                                );


            _criterioEspacio = new Predicate<Articulo>(a =>
                                    _espacioSeleccionado != null &&
                                    a.Espacio == _espacioSeleccionado.Idespacio
                                );

        }

        private void AddCriterios()
        {
            // Limpiar la lista de criterios antes de agregar los nuevos criterios
            _criterios.Clear();
            // Agrega el criterio de filtro por fecha de alta solo si la fecha inicial es menor o igual a la fecha final
            if (_fechaInicial <= _fechaFinal)
            {
                _criterios.Add(_criterioFecha);
            }
            // Agrega el criterio de filtro por número de serie solo si el texto del número de serie no está vacío
            if (!string.IsNullOrEmpty(_textoNumeroSerie))
            {
                _criterios.Add(_criterioNumeroSerie);
            }
            // Agrega el criterio de filtro por espacio solo si se ha seleccionado un espacio
            if (_espacioSeleccionado != null)
            {
                _criterios.Add(_criterioEspacio);
            }

        }

        // Metodo para quedarme con los articulos que cumplen los filtros
        private bool FiltroCriterios(object item)
        {
            Articulo articulo = (Articulo)item;

            if (_criterios != null && !_criterios.TrueForAll(x => x(articulo)))
                return false;

                _contadorResultados++;

                return _contadorResultados <= totalResultados;
        }
        #endregion
        #endregion
        // Método para inicializar los datos necesarios para el ViewModel
        public async Task Inicializa()
        {
            try
            {
                await InicializaListas();
                InicializaCriterios();
                _predicadoFiltros = new Predicate<object>(FiltroCriterios);
            }
            catch (Exception ex)
            {
                MensajeError.Mostrar("GESTIÓN MODELOS ARTÍCULO", "Error al cargar los tipos de modelo de artículos\n" +
                    "No puedo conectar con la base de datos", 0);
            }
        }

        public async Task<bool> GuardarArticuloAsync()
        {
            bool correcto = true;
            try
            {
                if (articulo.Idarticulo == 0)
                {
                    var ultimoId = await _articuloRepository.GetUltimoIdAsync();
                    articulo.Idarticulo = ultimoId + 1;
                    await _articuloRepository.AddAsync(articulo);
                }
                else
                {
                    await _articuloRepository.UpdateAsync(articulo);
                }
            }
            catch (Exception ex)
            {
                MensajeError.Mostrar("GESTIÓN ARTÍCULOS", "Error al guardar el artículo\n" +
                    "No puedo conectar con la base de datos", 0);
                correcto = false;
            }
            return correcto;
        }

        public void Filtrar()
        {
            // Reiniciar el contador de resultados antes de aplicar el filtro
            _contadorResultados = 0;

            AddCriterios();
            
            listaArticulos.Filter = _predicadoFiltros;
        }

        public void LimpiarFiltros()
        {
            listaArticulos.Filter = null;
            totalResultados = maximoResultados;
            // Reiniciar las fechas a su valor inicial (puedes ajustarlo según tus necesidades)
            fechaInicial = DateTime.UtcNow;
            fechaFinal = DateTime.UtcNow;
            // Reiniciar el texto del número de serie
            textoNumeroSerie = string.Empty;
            // Reiniciar el espacio seleccionado
            espacioSeleccionado = null;

        }

    }
}
