using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Millarow.Presentation.WPF.Framework
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        protected bool SetPropertyValue<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            propertyName.AssertNotNull(nameof(propertyName));

            if (!Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);

                return true;
            }

            return false;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
