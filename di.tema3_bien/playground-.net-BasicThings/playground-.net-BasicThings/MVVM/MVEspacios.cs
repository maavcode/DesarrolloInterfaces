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
        public MVEspacios(
            EspacioRepository espacioRepository)
        {
            
            _espacioRepository = espacioRepository;
        }

        public async Task Inicializa()
        {
            try
            {
                _listaEspacios = await _espacioRepository.GetAllAsync();
            }
            catch
            {
                MensajeError.Mostrar($"GESTION ESPACIOS", "Error al cargar los Espacios\n" +
                    "No se pudo conectar a la base de datos");
            }
            
        }
    }
}
