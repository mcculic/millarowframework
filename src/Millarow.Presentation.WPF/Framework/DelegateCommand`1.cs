using System;

namespace Millarow.Presentation.WPF.Framework
{
    public class DelegateCommand<T> : AbstractCommand<T>
    {
        private readonly Action<T> _execute;
        private bool _isEnabled;

        public DelegateCommand(Action<T> execute)
        {
            execute.AssertNotNull(nameof(execute));

            _execute = execute;
            _isEnabled = true;
        }

        public override bool CanExecute()
        {
            return _isEnabled;
        }

        public override void Execute(T parameter)
        {
            _execute(parameter);
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged(nameof(IsEnabled));
                    OnCanExecuteChanged();
                }
            }
        }
    }
}