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
        public List<Articulo> listaArticulos => _listaArticulos;
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
            // Cada vez que ejecute el dalogo es uno diferente, el repositorio no
            _articulo = new Articulo();
            _articuloRepository = articuloRepository;
            _espacioRepository = espacioRepository;
            _departamentoRepository = departamentoRepository;
            _usuarioRepository = usuarioRepository;
            _tipoArticuloRepository = tipoArticuloRepository;
            _modeloArticuloRepository = modeloArticuloRepository;
        }
        // Método para inicializar los datos necesarios para el ViewModel
        public async Task Inicializa()
        {
            try
            {
                _listaTipoArticulos = await GetAllAsync<Tipoarticulo>(_tipoArticuloRepository);
                _listaModeloArticulos = await GetAllAsync<Modeloarticulo>(_modeloArticuloRepository);
                _listaEstados = new List<string> { "Nuevo", "Usado", "Dañado"};
                _listaArticulos = await GetAllAsync<Articulo>(_articuloRepository);
                _listaEspacios = await GetAllAsync<Espacio>(_espacioRepository);
                _listaDepartamentos = await GetAllAsync<Departamento>(_departamentoRepository);
                _listaUsuarios = await GetAllAsync<Usuario>(_usuarioRepository);
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

    }
}
