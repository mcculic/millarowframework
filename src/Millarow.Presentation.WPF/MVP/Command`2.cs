using Millarow.Presentation.MVP;
using Millarow.Presentation.WPF.Framework;
using System;

namespace Millarow.Presentation.WPF.MVP
{
    public class Command<T, C> : AbstractCommand<T, C>, ICommand<T, C>
        where C : T
    {
        public override bool CanExecute(C parameter)
        {
            return Handler != null && Condition?.Invoke(parameter) != false;
        }

        public override void Execute(T parameter)
        {
            Handler?.Invoke(parameter);
        }

        void IConditional<C>.InvalidateCondition() => OnCanExecuteChanged();

        public Func<C, bool> Condition { get; set; }

        public Action<T> Handler { get; set; }
    }
}