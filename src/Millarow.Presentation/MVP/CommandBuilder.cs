using Millarow.Presentation.MVP.Fluent;
using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP
{
    public static class CommandBuilder
    {
        public static SyncCommandBuilder Attach(this ICommand command, Action handler)
        {
            command.AssertNotNull(nameof(command));
            handler.AssertNotNull(nameof(handler));

            command.Handler = handler;

            return new SyncCommandBuilder(command);
        }

        public static SyncCommandBuilder<T> Attach<T>(this ICommand<T> command, Action<T> handler)
        {
            command.AssertNotNull(nameof(command));
            handler.AssertNotNull(nameof(handler));

            command.Handler = handler;

            return new SyncCommandBuilder<T>(command);
        }

        public static SyncCommandBuilder<T, C> Attach<T, C>(this ICommand<T, C> command, Action<T> handler)
        {
            command.AssertNotNull(nameof(command));
            handler.AssertNotNull(nameof(handler));

            command.Handler = handler;

            return new SyncCommandBuilder<T, C>(command);
        }

        public static AsyncCommandBuilder Attach(this IAsyncCommand command, Func<Task> handler)
        {
            command.AssertNotNull(nameof(command));
            handler.AssertNotNull(nameof(handler));

            command.Handler = handler;

            return new AsyncCommandBuilder(command);
        }

        public static AsyncCommandBuilder<T> Attach<T>(this IAsyncCommand<T> command, Func<T, Task> handler)
        {
            command.AssertNotNull(nameof(command));
            handler.AssertNotNull(nameof(handler));

            command.Handler = handler;

            return new AsyncCommandBuilder<T>(command);
        }
    }
}
