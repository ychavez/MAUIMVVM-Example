using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MiToDoListApp.Mobile.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private bool _isBusy;
        public Dictionary<string, List<string>> Errors { get; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        // Propiedad IsBusy para estados de carga
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        // Implementación de INotifyDataErrorInfo
        public bool HasErrors => Errors.Any();

        public IEnumerable GetErrors(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return Errors.SelectMany(e => e.Value);

            return Errors.TryGetValue(propertyName, out var errors)
                ? errors
                : Enumerable.Empty<string>();
        }

        // Método para actualizar propiedades y notificar cambios
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        // Notificar cambios en propiedades
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Notificar cambios en los errores de una propiedad
        protected void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        // Limpiar todos los errores
        protected void ClearAllErrors()
        {
            Errors.Clear();
            OnErrorsChanged(string.Empty); // Notificar que todos los errores cambiaron
        }
    }
}
