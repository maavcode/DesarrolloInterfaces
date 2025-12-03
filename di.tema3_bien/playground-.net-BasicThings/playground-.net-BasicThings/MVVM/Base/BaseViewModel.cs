using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace playground_.net_BasicThings.MVVM.Base
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// Método que se ejecuta cuando se produce un cambio en una propiedad
        /// Permite notificar a la vista que debe actualizarse
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad que debe de actualizarse</param>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) // IMPORTANTE
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            RaiseCommandsCanExecuteChanged();
        }

        /// <summary>
        /// Notifica a WPF que debe reevaluar la disponibilidad de los comandos.
        /// </summary>
        protected void RaiseCommandsCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Método auxiliar para establecer el valor y notificar el cambio solo si es necesario
        /// Reduce el código repetido al establecer propiedades en ViewModels
        /// </summary>
        /// <typeparam name="T">Clase genérica</typeparam>
        /// <param name="field">Propiedad que debe de actualizarse</param>
        /// <param name="value">Valor con el que se actualiza</param>
        /// <param name="propertyName">Nombre de la propiedad</param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
