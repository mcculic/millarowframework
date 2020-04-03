using System;
using System.Windows.Input;

namespace Millarow.Presentation.WPF.Framework
{
    public abstract class AbstractCommand<T> : BindableBase, ICommand
    {
        public abstract bool CanExecute();

        public abstract void Execute(T parameter);

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            if (parameter == null && default(T) == null)
                Execute(default(T));
            else if (parameter is T p)
                Execute(p);
        }

        public event EventHandler CanExecuteChanged;
    }
}
