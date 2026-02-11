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
    public class MVEspacios : MVBase
    {
        private readonly EspacioRepository _espacioRepository;
        private List<Espacio> _listaEspacios;
        public List<Espacio> listaEspacios => _listaEspacios;

        private UsuarioRepository _usuarioRepository;
        private ModeloArticuloRepository _modeloArticuloRepository;
        private DepartamentoRepository _departamentoRepository;
        private ArticuloRepository _articuloRepository;

        private List<Departamento> _listaDepartamentos;
        public List<Departamento> listaDepartamentos => _listaDepartamentos;
        
        private List<Modeloarticulo> _listaModelosArticulo;
        public List<Modeloarticulo> listaModelosArticulo => _listaModelosArticulo;

        private List<Usuario> _listaUsuarios;
        public List<Usuario> listaUsuarios => _listaUsuarios;

        private List<string> _listaEstados;
        public List<string> listaEstados => _listaEstados;

        private Articulo _articuloSeleccionado;
        public Articulo articuloSeleccionado
        {
            get => _articuloSeleccionado;
            set { _articuloSeleccionado = value;
                OnPropertyChanged(nameof(articuloSeleccionado));
            }
        }

        public MVEspacios(
            EspacioRepository espacioRepository,
            DepartamentoRepository departamentoRepository,
            ArticuloRepository articuloRepository,
            UsuarioRepository usuarioRepository,
            ModeloArticuloRepository modeloArticuloRepository)
        {
            
            _espacioRepository = espacioRepository;
            _departamentoRepository = departamentoRepository;
            _usuarioRepository = usuarioRepository;
            _modeloArticuloRepository = modeloArticuloRepository;
            _articuloRepository = articuloRepository;

        }

        private async Task InicializaListas()
        {
            _listaEspacios = await _espacioRepository.GetAllAsync();
            _listaDepartamentos = await _departamentoRepository.GetAllAsync();
            _listaModelosArticulo = await _modeloArticuloRepository.GetAllAsync();
            _listaUsuarios = await _usuarioRepository.GetAllAsync();
            _listaEstados = new List<string> { "Nuevo", "Usado", "Reparación", "Baja" };
            OnPropertyChanged(nameof(listaEstados));
        }

        public async Task Inicializa()
        {
            try
            {
                await InicializaListas();
            }
            catch
            {
                MensajeError.Mostrar($"GESTION ESPACIOS", "Error al cargar los Espacios\n" +
                    "No se pudo conectar a la base de datos");
            }
            
        }
    }
}
