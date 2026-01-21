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


        #endregion

        #region Getters y Setters
        public Modeloarticulo modeloArticulo
        {
            get => _modeloArticulo;
            set => SetProperty(ref _modeloArticulo, value);
        }
        public List<Tipoarticulo> listaTipoArticulos => _listaTipoArticulos;
        public List<Modeloarticulo> listaModelosArticulo => _listaModelosArticulo;

        #endregion

        // Constructor que recibe el modelo de artículo y el repositorio como parámetros
        public MVModeloArticulo( 
            ModeloArticuloRepository modeloArticuloRepository,
            TipoArticuloRepository tipoArticuloRepository
            )
        {
            // Cada vez que ejecute el dalogo es uno diferente, el repositorio no
            _modeloArticulo = new Modeloarticulo();
            _modeloArticuloRepository = modeloArticuloRepository;
            _tipoArticuloRepository = tipoArticuloRepository;
        }

        // Método para inicializar los datos necesarios para el ViewModel
        public async Task Inicializa()
        {
            try
            {
                _listaTipoArticulos = await GetAllAsync<Tipoarticulo>(_tipoArticuloRepository);
                _listaModelosArticulo = await GetAllAsync<Modeloarticulo>(_modeloArticuloRepository);
            }
            catch (Exception ex)
            {
                MensajeError.Mostrar("GESTIÓN MODELOS ARTÍCULO", "Error al cargar los tipos de modelo de artículos\n" +
                    "No puedo conectar con la base de datos", 0);
            }
        }

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
    }
}
