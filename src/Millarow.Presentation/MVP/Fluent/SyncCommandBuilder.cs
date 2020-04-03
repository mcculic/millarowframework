using System;

namespace Millarow.Presentation.MVP.Fluent
{
    public class SyncCommandBuilder
    {
        private ICommand _command;

        public SyncCommandBuilder(ICommand command)
        {
            command.AssertNotNull(nameof(command));

            _command = command;
        }

        public SyncCommandBuilder When(Func<bool> condition)
        {
            condition.AssertNotNull(nameof(condition));

            var current = _command.Condition;

            _command.Condition = () => current?.Invoke() != false && condition();

            return this;
        }

        public SyncCommandBuilder Wrap(Func<Action, Action> wrapper)
        {
            wrapper.AssertNotNull(nameof(wrapper));

            var handler = _command.Handler;

            _command.Handler = () => wrapper(handler);

            return this;
        }

        public SyncCommandBuilder Catch<TException>(Action<TException> onException)
            where TException : Exception
        {
            var handler = _command.Handler;

            _command.Handler = () =>
            {
                try
                {
                    handler();
                }
                catch (TException ex)
                {
                    onException?.Invoke(ex);
                }
            };

            return this;
        }

        public SyncCommandBuilder MayCancel()
        {
            return Catch<OperationCanceledException>(null);
        }
    }
}
