using MaterialDesignThemes.Wpf;
using playground_.net_BasicThings.Backend.Servicios;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace playground_.net_BasicThings.MVVM.Base
{
    public class MVBase : ValidatableViewModel
    {
        /// <summary>
        /// Botón del formulario que queremos que se active/desactive en función
        /// de si hay errores en la validación de los campos
        /// </summary>
        public bool HasErrors
        {
            get => _hasErrors;
            set => SetProperty(ref _hasErrors, value);
        }

        private bool _hasErrors;


        /// <summary>
        /// Variable que llev la cuenta de los errores que hay en el formulario
        /// </summary>
        private int errorCount = 0;

        public SnackbarMessageQueue SnackbarMessageQueue { get; } = new SnackbarMessageQueue(TimeSpan.FromSeconds(3));

        // Añade métodos que nos ayudan en la validación -------------------------------------------------------------------------------
        /// <summary>
        /// Método que nos permite saber si hay errores en algún formulario
        /// </summary>
        /// <param name="obj">Ventana o panel que contiene los controles del formulario que queremos comprobar</param>
        /// <returns>n caso de que haya errores devolverá el valor de falso y en caso de que no haya devolverá verdadero</returns>
        public bool IsValid(DependencyObject obj)
        {
            // The dependency object is valid if it has no errors and all
            // of its children (that are dependency objects) are error-free.
            return !Validation.GetHasError(obj) &&
            LogicalTreeHelper.GetChildren(obj)
            .OfType<DependencyObject>()
            .All(IsValid);
        }

        /// <summary>
        /// Manejador de eventos para la validación de errores en los controles del formulario.
        /// Si se añade un error, incrementa el contador de errores; si se elimina, lo decrementa.
        /// Luego utiliza la propiedad HasErrors para activar o desactivar el botón de guardar.
        /// </summary>
        /// <param name="sender">Control que produce el error de validación</param>
        /// <param name="e">Parámetros del error</param>
        public void OnErrorEvent(object sender, RoutedEventArgs e)
        {
            var validationEventArgs = e as ValidationErrorEventArgs;
            if (validationEventArgs == null)
                throw new Exception("Argumentos inesperados");
            switch (validationEventArgs.Action)
            {
                case ValidationErrorEventAction.Added:
                    {
                        errorCount++; break;
                    }
                case ValidationErrorEventAction.Removed:
                    {
                        errorCount--; break;
                    }
                default:
                    {
                        throw new Exception("Acción desconocida");
                    }
            }
            UpdatehasErrors();
            //btnGuardar.IsEnabled = errorCount == 0;
        }

        private void UpdatehasErrors()
        {
            HasErrors = errorCount > 0;
        }
        // Métodos CRUD genéricos asíncronos con manejo de excepciones
        /// <summary>
        /// Obtiene la lista asociada a una tabla de la base de datos
        /// </summary>
        /// <typeparam name="T">Entidad que representa el objeto asociado a la tabla</typeparam>
        /// <param name="repo">Repositorio utilizado para el acceso a datos</param>
        /// <returns>La lista con los objetos de la tabla o bien una lista vacía en caso de que haya un problema</returns>
        protected async Task<List<T>> GetAllAsync<T>(IGenericRepository<T> repo) where T : class
        {
            IEnumerable<T> result;
            try
            {
                result = await repo.GetAllAsync();
            }
            catch (Exception ex)
            {
                result = new List<T>();
                throw new Exception("Database connection error occurred while fetching all elements.", ex);
            }
            return result.ToList();
        }

        protected async Task<T?> GetByIdAsync<T>(IGenericRepository<T> repo, int id) where T : class
        {
            try
            {
                return await repo.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                SnackbarMessageQueue.Enqueue($"Error al obtener elemento por ID: {ex.Message}");
                return null;
            }
        }

        protected async Task<bool> AddAsync<T>(IGenericRepository<T> repo, T entity) where T : class
        {
            try
            {
                await repo.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                SnackbarMessageQueue.Enqueue($"Error al añadir elemento: {ex.Message}");
                return false;
            }
        }

        protected async Task<bool> UpdateAsync<T>(IGenericRepository<T> repo, T entity) where T : class
        {
            try
            {
                await repo.UpdateAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                SnackbarMessageQueue.Enqueue($"Error al actualizar elemento: {ex.Message}");
                return false;
            }
        }

        protected async Task<bool> DeleteAsync<T>(IGenericRepository<T> repo, int id) where T : class
        {
            try
            {
                await repo.RemoveByIdAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                SnackbarMessageQueue.Enqueue($"Error al eliminar elemento: {ex.Message}");
                return false;
            }
        }

        protected async Task<bool> AddOrUpdateAsync<T>(IGenericRepository<T> repo, T entity) where T : class
        {
            try
            {
                // Obtener la propiedad "Id" (puedes cambiar el nombre si usas otro identificador)
                var idProp = typeof(T).GetProperty("Id");
                if (idProp == null)
                {
                    SnackbarMessageQueue.Enqueue("No se encontró la propiedad 'Id' en el tipo.");
                    return false;
                }

                var idValue = idProp.GetValue(entity);
                if (idValue == null || (int)idValue == 0)
                {
                    // Si el Id es nulo o 0, se considera nuevo
                    await repo.AddAsync(entity);
                    return true;
                }

                var existing = await repo.GetByIdAsync((int)idValue);
                if (existing == null)
                {
                    await repo.AddAsync(entity);
                }
                else
                {
                    await repo.UpdateAsync(entity);
                }
                return true;
            }
            catch (Exception ex)
            {
                SnackbarMessageQueue.Enqueue($"Error al añadir o actualizar elemento: {ex.Message}");
                return false;
            }
        }

    }
}
