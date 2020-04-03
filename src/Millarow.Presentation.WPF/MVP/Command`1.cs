using Millarow.Presentation.MVP;
using Millarow.Presentation.WPF.Framework;
using System;

namespace Millarow.Presentation.WPF.MVP
{
    public class Command<T> : AbstractCommand<T>, ICommand<T>
    {
        public override bool CanExecute()
        {
            return Handler != null && Condition?.Invoke() != false;
        }

        public override void Execute(T parameter)
        {
            Handler?.Invoke(parameter);
        }

        void IConditional.InvalidateCondition() => OnCanExecuteChanged();

        public Func<bool> Condition { get; set; }

        public Action<T> Handler { get; set; }
    }
}