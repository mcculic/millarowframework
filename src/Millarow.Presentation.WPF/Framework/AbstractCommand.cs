using System;
using System.Windows.Input;

namespace Millarow.Presentation.WPF.Framework
{
    public abstract class AbstractCommand : BindableBase, ICommand
    {
        public abstract bool CanExecute();

        public abstract void Execute();

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter) => CanExecute();

        void ICommand.Execute(object parameter) => Execute();

        public event EventHandler CanExecuteChanged;
    }
}