using System;
using System.Threading;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP.Fluent
{
    public class AsyncCommandBuilder<T, C>
    {
        private IAsyncCommand<T, C> _command;

        public AsyncCommandBuilder(IAsyncCommand<T, C> command)
        {
            command.AssertNotNull(nameof(command));

            _command = command;
        }

        
        public AsyncCommandBuilder<T, C> Wrap(Func<Func<T, Task>, T, Task> wrapper)
        {
            wrapper.AssertNotNull(nameof(wrapper));

            var handler = _command.Handler;

            _command.Handler = p => wrapper(handler, p);

            return this;
        }

        public AsyncCommandBuilder<T, C> IgnoreReentry()
        {
            var isExecuting = new VolatileFlag();
            var handler = _command.Handler;

            _command.Handler = async p =>
            {
                if (isExecuting.Value)
                    return;

                isExecuting.Value = true;

                try
                {
                    await handler(p);
                }
                finally
                {
                    isExecuting.Value = false;
                }
            };

            return this;
        }

        public AsyncCommandBuilder<T, C> CheckReentry()
        {
            var isExecuting = new VolatileFlag();
            var handler = _command.Handler;

            _command.Handler = async p =>
            {
                if (isExecuting.Value)
                    throw Exceptions.ReentrancyCheckFailed();

                isExecuting.Value = true;

                try
                {
                    await handler(p);
                }
                finally
                {
                    isExecuting.Value = false;
                }
            };

            return this;
        }

        public AsyncCommandBuilder<T, C> Synchronized(EventWaitHandle waitHandle)
        {
            waitHandle.AssertNotNull(nameof(waitHandle));

            var handler = _command.Handler;

            _command.Handler = async p =>
            {
                waitHandle.WaitOne();

                try
                {
                    await handler(p);
                }
                finally
                {
                    waitHandle.Set();
                }
            };

            return this;
        }

        public AsyncCommandBuilder<T, C> Ignore<TException>()
            where TException : Exception
        {
            var handler = _command.Handler;

            _command.Handler = async p =>
            {
                try
                {
                    await handler(p);
                }
                catch (TException)
                {
                }
            };

            return this;
        }

        public AsyncCommandBuilder<T, C> MayCancel()
        {
            return Ignore<OperationCanceledException>();
        }
    }
}
