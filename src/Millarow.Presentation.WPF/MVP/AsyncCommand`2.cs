using Millarow.Presentation.MVP;
using Millarow.Presentation.WPF.Framework;
using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.WPF.MVP
{
    public class AsyncCommand<T, C> : AbstractCommand<T, C>, IAsyncCommand<T, C>
        where C : T
    {
        public override bool CanExecute(C parameter)
        {
            return Handler != null && Condition?.Invoke(parameter) != false;
        }

        public override async void Execute(T parameter)
        {
            await Handler?.Invoke(parameter);
        }

        void IConditional<C>.InvalidateCondition() => OnCanExecuteChanged();

        public Func<C, bool> Condition { get; set; }

        public Func<T, Task> Handler { get; set; }
    }
}