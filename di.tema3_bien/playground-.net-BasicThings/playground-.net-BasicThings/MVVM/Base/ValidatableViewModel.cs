using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace playground_.net_BasicThings.MVVM.Base
{
    public abstract class ValidatableViewModel : BaseViewModel, IDataErrorInfo
    {
        // Implementación de IDataErrorInfo
        /// <summary>
        /// Propiedad que indica si el objeto es válido o no
        /// En principio devuelve null
        /// </summary>
        public virtual string Error => null!;
        /// <summary>
        /// Propiedad que permite validar las propiedades del objeto
        /// </summary>
        /// <param name="columnName">Nombre de la propiedad o atributo del objeto </param>
        /// <returns>Devuelve el primer mensaje de error que encuentra.
        ///          Si no hay errores, entonces devuelve la cadena vacía</returns>
        public virtual string this[string columnName]
        {
            get
            {
                var validationResults = new List<ValidationResult>();

                if (Validator.TryValidateProperty(
                        GetType().GetProperty(columnName).GetValue(this)
                        , new ValidationContext(this)
                        {
                            MemberName = columnName
                        }
                        , validationResults))
                    return null;

                return validationResults.First().ErrorMessage;
            }
        }

        /// <summary>
        /// Método que debe ser sobreescrito en los ViewModels derivados para validar propiedades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad a validar</param>
        /// <returns>Mensaje de error o string vacío si no hay error</returns>
        // Puedes seguir sobrescribiendo este método en los hijos si necesitas validaciones adicionales
        protected virtual string ValidateProperty(string propertyName) => string.Empty;

        /// <summary>
        /// Método para notificar a la vista que debe reevaluar la disponibilidad de los comandos.
        /// </summary>
        protected void RaiseCommandsCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
