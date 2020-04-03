using Millarow.Presentation.MVP;
using Millarow.Presentation.WPF.Framework;
using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.WPF.MVP
{
    public class AsyncCommand<T> : AbstractCommand<T>, IAsyncCommand<T>
    {
        public override bool CanExecute()
        {
            return Handler != null && Condition?.Invoke() != false;
        }

        public override async void Execute(T parameter)
        {
            await Handler?.Invoke(parameter);
        }

        void IConditional.InvalidateCondition() => OnCanExecuteChanged();

        public Func<bool> Condition { get; set; }

        public Func<T, Task> Handler { get; set; }
    }
}