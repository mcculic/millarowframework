using System;

namespace Millarow.Presentation.WPF.Framework
{
    public class DelegateCommand : AbstractCommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public DelegateCommand(Action execute, Func<bool> canExecute = null)
        {
            execute.AssertNotNull(nameof(execute));

            _execute = execute;
            _canExecute = canExecute;
        }

        public override bool CanExecute()
        {
            return _canExecute?.Invoke() != false;
        }

        public override void Execute()
        {
            _execute();
        }

        public void UpdateCanExecute()
        {
            OnCanExecuteChanged();
        }
    }
}