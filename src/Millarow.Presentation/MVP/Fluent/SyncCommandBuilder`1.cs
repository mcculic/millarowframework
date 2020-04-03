using System;

namespace Millarow.Presentation.MVP.Fluent
{
    public class SyncCommandBuilder<T>
    {
        private ICommand<T> _command;

        public SyncCommandBuilder(ICommand<T> command)
        {
            command.AssertNotNull(nameof(command));

            _command = command;
        }

        public SyncCommandBuilder<T> When(Func<bool> condition)
        {
            condition.AssertNotNull(nameof(condition));

            var current = _command.Condition;

            _command.Condition = () => current?.Invoke() != false && condition();

            return this;
        }

        public SyncCommandBuilder<T> Wrap(Func<Action<T>, Action<T>> wrapper)
        {
            wrapper.AssertNotNull(nameof(wrapper));

            var handler = _command.Handler;

            _command.Handler = p => wrapper(handler);

            return this;
        }

        public SyncCommandBuilder<T> Catch<TException>(Action<TException> onException)
            where TException : Exception
        {
            var handler = _command.Handler;

            _command.Handler = p =>
            {
                try
                {
                    handler(p);
                }
                catch (TException ex)
                {
                    onException?.Invoke(ex);
                }
            };

            return this;
        }

        public SyncCommandBuilder<T> MayCancel()
        {
            return Catch<OperationCanceledException>(null);
        }
    }
}
