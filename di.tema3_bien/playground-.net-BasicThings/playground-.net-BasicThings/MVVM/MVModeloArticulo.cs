using di.proyecto.clase._2025.Backend.Servicios_Repositorio_;
using playground_.net_BasicThings.Backend.Servicios;
using playground_.net_BasicThings.Frontend.Mensajes;
using playground_.net_BasicThings.MVVM.Base;
using playground_.net_BasicThings.Backend.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace playground_.net_BasicThings.MVVM
{
    public class MVModeloArticulo : MVBase
    {
        #region Campos y propiedades privadas
        // Propiedad que guarda el modelo de artículo actual, la propiedad que guarda el repositorio del mismo y la lista de modelos de artículo
        private Modeloarticulo _modeloArticulo;
        private ModeloArticuloRepository _modeloArticuloRepository;
        private List<Modeloarticulo> _listaModelosArticulo;
        // Propiedad que guarda el repositorio de tipos de artículo y la lista de tipos de artículo
        private List<Tipoarticulo> _listaTipoArticulos;
        private TipoArticuloRepository _tipoArticuloRepository;
        // Lista de criterios de filtrado para los modelos de artículo
        private List<Predicate<Modeloarticulo>> _criterios;
        //
        private Tipoarticulo _tipoArticuloSeleccionado;
        //
        private Predicate<Modeloarticulo> _criterioTipoArticulo;
        //
        private Predicate<object> _predicadoFiltros;

        private String _textoNombre;

        private Predicate<Modeloarticulo> _criterioNombreTipo;

        #endregion

        #region Getters y Setters
        public Modeloarticulo modeloArticulo
        {
            get => _modeloArticulo;
            set => SetProperty(ref _modeloArticulo, value);
        }
        public List<Tipoarticulo> listaTipoArticulos => _listaTipoArticulos;
        // public List<Modeloarticulo> listaModelosArticulo => _listaModelosArticulo;

        // NECESARIO PARA ENLAZAR CON EL COMBOBOX EN LA VISTA
        public ListCollectionView listaModelosArticulo { get; set; }

        public Tipoarticulo tipoarticuloSeleccionado {
            get => _tipoArticuloSeleccionado;
            set => SetProperty(ref _tipoArticuloSeleccionado, value);
        }

        public String textoNombre
        {
            get => _textoNombre;
            set => SetProperty(ref _textoNombre, value);
        }

        #endregion

        // Constructor que recibe el modelo de artículo y el repositorio como parámetros
        public MVModeloArticulo( 
            ModeloArticuloRepository modeloArticuloRepository,
            TipoArticuloRepository tipoArticuloRepository
            )
        {
            // Cada vez que ejecute el dalogo es uno diferente, el repositorio no
            // _modeloArticulo = new Modeloarticulo(); Ya no es necesario
            _modeloArticuloRepository = modeloArticuloRepository;
            _tipoArticuloRepository = tipoArticuloRepository;
        }

        // Método para inicializar los datos necesarios para el ViewModel
        public async Task Inicializa()
        {
            try
            {
                // Inicializa las listas
                await InicializaListas();
                // Inicializa los criterios de filtrado
                InicializaCriterios();
                //
                _predicadoFiltros = new Predicate<object>(FiltroCriterios);
            }
            catch (Exception ex)
            {
                MensajeError.Mostrar("GESTIÓN MODELOS ARTÍCULO", "Error al cargar los tipos de modelo de artículos\n" +
                    "No puedo conectar con la base de datos", 0);
            }
        }
        #region Metodos privados
        private void InicializaCriterios()
        {
            // Instancia el criterio de filtrado por tipo de artículo
            _criterioTipoArticulo = new Predicate<Modeloarticulo>(m => m.TipoNavigation != null
                                    && m.TipoNavigation.Equals(_tipoArticuloSeleccionado));

            _criterioNombreTipo = new Predicate<Modeloarticulo>(m => !string.IsNullOrEmpty(_textoNombre) 
                                    && m.Nombre!.ToLower().StartsWith(_textoNombre.ToLower()));

        }

        private void AddCriterios()
        {
            // Borramos la lista de criterios
            _criterios.Clear();
            // Añadimos los criterios segun los filtros seleccionados
            if (tipoarticuloSeleccionado != null)
            {
                _criterios.Add(_criterioTipoArticulo);
            }
            if (!string.IsNullOrEmpty(textoNombre)){
                _criterios.Add(_criterioNombreTipo);
            }
        }
        private bool FiltroCriterios(object item)
        {
            bool correcto = true;
            // 
            Modeloarticulo modelo = (Modeloarticulo)item;
            // 
            if (_criterios != null) // Si no hay criterio no filtra
            {
                // Coge el objeto modeloArticulo y comprueba si el tipoArticulo es el seleccionado
                correcto = _criterios.TrueForAll(x => x(modelo));
            }
            return correcto;
        }

        private async Task  InicializaListas()
        {
            _listaTipoArticulos = await GetAllAsync<Tipoarticulo>(_tipoArticuloRepository);
            _listaModelosArticulo = await GetAllAsync<Modeloarticulo>(_modeloArticuloRepository);
            // NECESARIO PARA ENLAZAR CON EL COMBOBOX EN LA VISTA
            listaModelosArticulo = new ListCollectionView(_listaModelosArticulo);
            // Inicializamos la lista de criterios de filtrado
            _criterios = new List<Predicate<Modeloarticulo>>();
        }
        #endregion

        // Método para guardar el modelo de artículo en la base de datos
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

        public void Filtrar()
        {
            // Actualizamos los criterios de filtrado
            AddCriterios();
            // Lanzamos el proceso de filtrado
            listaModelosArticulo.Filter = _predicadoFiltros;
        }

        public void LimpiarFiltros()
        {
            // Reseteamos los criterios de filtrado
            tipoarticuloSeleccionado = null;
            textoNombre = string.Empty;
            // Reseteamos el filtro de la lista
            listaModelosArticulo.Filter = null;
        }
    }
}
