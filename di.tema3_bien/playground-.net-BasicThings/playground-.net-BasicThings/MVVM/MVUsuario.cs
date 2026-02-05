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
    public class MVUsuario : MVBase
    {
        #region Campos y propiedades privadas
        // Propiedad que guarda el usuario actual y propiedad que guarda el repositorio del mismo
        private Usuario _usuario;
        private UsuarioRepository _usuarioRepository;

        private Tipousuario _tipoUsuario;
        private GenericRepository<Tipousuario> _tipousuarioRepository;

        private Rol _rol;
        private GenericRepository<Rol> _rolRepository;

        // Listas de datos para los combo box
        private List<Tipousuario> _listaTiposUsuarios;
        private List<Rol> _listaRoles;

        private List<Usuario> _listaUsuarios;

        #endregion
        #region Getters y Setters
        public Usuario usuario
        {
            get => _usuario;
            set => SetProperty(ref _usuario, value);
        }
        // Funciones getter y setter para los combo box
        public List<Tipousuario> listaTiposUsuarios => _listaTiposUsuarios;
        public List<Rol> listaRoles => _listaRoles;
        public ListCollectionView listaUsuarios { get; set; }

        #endregion
        public MVUsuario(
            UsuarioRepository usuarioRepository,
            GenericRepository<Tipousuario> tipousuarioRepository,
            GenericRepository<Rol> rolRepository
            )
        {
            _usuario = new Usuario();
            _usuarioRepository = usuarioRepository;
            _tipousuarioRepository = tipousuarioRepository;
            _rolRepository = rolRepository;
        }

        public async Task Inicializa()
        {
            try
            {
                _listaTiposUsuarios = await GetAllAsync<Tipousuario>(_tipousuarioRepository);
                _listaRoles = await GetAllAsync<Rol>(_rolRepository);

                _listaUsuarios = await GetAllAsync<Usuario>(_usuarioRepository);
                listaUsuarios = new ListCollectionView(_listaUsuarios);
            }
            catch (Exception ex)
            {
                MensajeError.Mostrar("GESTIÓN USUARIOS", "No puedo conectar con la base de datos", 0);
            }
        }

        public async Task<bool> GuardarActualizarUsuarioAsync()
        {
            bool correcto = true;
            try
            {
                if (usuario.Idusuario == 0)
                {
                    await _usuarioRepository.AddAsync(usuario);
                }
                else
                {
                    await _usuarioRepository.UpdateAsync(usuario);
                }
            }
            catch (Exception ex)
            {
                MensajeError.Mostrar("GESTIÓN USUARIOS", "Error al guardar el usuario\n" +
                    "No puedo conectar con la base de datos", 0);
                correcto = false;
            }
            return correcto;
        }
    }
}