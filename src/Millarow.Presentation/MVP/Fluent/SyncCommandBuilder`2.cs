using System;

namespace Millarow.Presentation.MVP.Fluent
{
    public class SyncCommandBuilder<T, C>
    {
        private ICommand<T, C> _command;

        public SyncCommandBuilder(ICommand<T, C> command)
        {
            command.AssertNotNull(nameof(command));

            _command = command;
        }

        public SyncCommandBuilder<T, C> When(Func<C, bool> condition)
        {
            condition.AssertNotNull(nameof(condition));

            var current = _command.Condition;

            _command.Condition = p => current?.Invoke(p) != false && condition(p);

            return this;
        }

        public SyncCommandBuilder<T, C> When(Func<bool> condition)
        {
            condition.AssertNotNull(nameof(condition));

            var current = _command.Condition;

            _command.Condition = p => current?.Invoke(p) != false && condition();

            return this;
        }

        public SyncCommandBuilder<T, C> Wrap(Func<Action<T>, Action<T>> wrapper)
        {
            wrapper.AssertNotNull(nameof(wrapper));

            var handler = _command.Handler;

            _command.Handler = p => wrapper(handler);

            return this;
        }

        public SyncCommandBuilder<T, C> Catch<TException>(Action<TException> onException)
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

        public SyncCommandBuilder<T, C> MayCancel()
        {
            return Catch<OperationCanceledException>(null);
        }
    }
}
