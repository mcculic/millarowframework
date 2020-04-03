using System;
using System.Windows.Input;

namespace Millarow.Presentation.WPF.Framework
{
    public abstract class AbstractCommand<T, C> : BindableBase, ICommand
        where C : T
    {
        public abstract bool CanExecute(C parameter);

        public abstract void Execute(T parameter);

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (parameter == null && default(C) == null)
                return CanExecute(default(C));

            if (parameter is C p)
                return CanExecute(p);

            return false;
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