using System;
using System.Threading;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP.Fluent
{
    public class AsyncCommandBuilder
    {
        private IAsyncCommand _command;

        public AsyncCommandBuilder(IAsyncCommand command)
        {
            command.AssertNotNull(nameof(command));

            _command = command;
        }

        public AsyncCommandBuilder When(Func<bool> condition)
        {
            condition.AssertNotNull(nameof(condition));

            var current = _command.Condition;

            _command.Condition = () => current?.Invoke() != false && condition();

            return this;
        }

        public AsyncCommandBuilder Wrap(Func<Func<Task>, Task> wrapper)
        {
            wrapper.AssertNotNull(nameof(wrapper));

            var handler = _command.Handler;

            _command.Handler = () => wrapper(handler);

            return this;
        }

        public AsyncCommandBuilder IgnoreReentry()
        {
            var isExecuting = new VolatileFlag();
            var handler = _command.Handler;

            _command.Handler = async () =>
            {
                if (isExecuting.Value)
                    return;

                isExecuting.Value = true;

                try
                {
                    await handler();
                }
                finally
                {
                    isExecuting.Value = false;
                }
            };

            return this;
        }

        public AsyncCommandBuilder CheckReentry()
        {
            var isExecuting = new VolatileFlag();
            var handler = _command.Handler;

            _command.Handler = async () =>
            {
                if (isExecuting.Value)
                    throw Exceptions.ReentrancyCheckFailed();

                isExecuting.Value = true;

                try
                {
                    await handler();
                }
                finally
                {
                    isExecuting.Value = false;
                }
            };

            return this;
        }

        public AsyncCommandBuilder Synchronized(EventWaitHandle waitHandle)
        {
            waitHandle.AssertNotNull(nameof(waitHandle));
            
            var handler = _command.Handler;

            _command.Handler = async () =>
            {
                waitHandle.WaitOne();

                try
                {
                    await handler();
                }
                finally
                {
                    waitHandle.Set();
                }
            };

            return this;
        }

        public AsyncCommandBuilder Catch<TException>(Action<TException> onException = null)
            where TException : Exception
        {
            var handler = _command.Handler;

            _command.Handler = async () =>
            {
                try
                {
                    await handler();
                }
                catch (TException ex)
                {
                    onException?.Invoke(ex);
                }
            };

            return this;
        }

        public AsyncCommandBuilder MayCancel()
        {
            return Catch<OperationCanceledException>(null);
        }
    }
}
