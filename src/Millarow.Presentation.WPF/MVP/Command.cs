using Millarow.Presentation.MVP;
using Millarow.Presentation.WPF.Framework;
using System;

namespace Millarow.Presentation.WPF.MVP
{
    public class Command : AbstractCommand, ICommand
    {
        public override bool CanExecute()
        {
            return Handler != null && Condition?.Invoke() != false;
        }

        public override void Execute()
        {
            Handler?.Invoke();
        }

        void IConditional.InvalidateCondition() => OnCanExecuteChanged();

        public Func<bool> Condition { get; set; }

        public Action Handler { get; set; }
    }
}
