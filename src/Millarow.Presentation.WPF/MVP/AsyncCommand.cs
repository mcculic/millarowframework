using Millarow.Presentation.MVP;
using Millarow.Presentation.WPF.Framework;
using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.WPF.MVP
{
    public class AsyncCommand : AbstractCommand, IAsyncCommand
    {
        public override bool CanExecute()
        {
            return Handler != null && Condition?.Invoke() != false;
        }

        public override async void Execute()
        {
            await Handler?.Invoke();
        }

        void IConditional.InvalidateCondition() => OnCanExecuteChanged();

        public Func<bool> Condition { get; set; }

        public Func<Task> Handler { get; set; }
    }
}
