using System;
using System.Threading;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP.Fluent
{
    public class AsyncCommandBuilder<T>
    {
        private IAsyncCommand<T> _command;

        public AsyncCommandBuilder(IAsyncCommand<T> command)
        {
            command.AssertNotNull(nameof(command));

            _command = command;
        }

        public AsyncCommandBuilder<T> When(Func<bool> condition)
        {
            condition.AssertNotNull(nameof(condition));

            var current = _command.Condition;

            _command.Condition = () => current?.Invoke() != false && condition();

            return this;
        }

        //public AsyncCommandBuilder<T> When(Func<T, bool> condition)
        //{
        //    condition.AssertNotNull(nameof(condition));

        //    var current = _command.Condition;

        //    _command.Condition = p => current?.Invoke(p) != false && condition(p);

        //    return this;
        //}

        public AsyncCommandBuilder<T> Wrap(Func<Func<T, Task>, T, Task> wrapper)
        {
            wrapper.AssertNotNull(nameof(wrapper));

            var handler = _command.Handler;

            _command.Handler = p => wrapper(handler, p);

            return this;
        }

        public AsyncCommandBuilder<T> IgnoreReentry()
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

        public AsyncCommandBuilder<T> CheckReentry()
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

        public AsyncCommandBuilder<T> Synchronized(EventWaitHandle waitHandle)
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

        public AsyncCommandBuilder<T> Ignore<TException>()
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

        public AsyncCommandBuilder<T> MayCancel()
        {
            return Ignore<OperationCanceledException>();
        }
    }
}
