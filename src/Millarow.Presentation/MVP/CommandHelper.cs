using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP
{
    public static class CommandHelper
    {
        #region Sync

        public static bool CanExecute(this ICommand command)
        {
            command.AssertNotNull(nameof(command));

            return command.Handler != null && command.Condition?.Invoke() != false;
        }

        public static bool CanExecute<T>(this ICommand<T> command)
        {
            command.AssertNotNull(nameof(command));

            return command.Handler != null && command.Condition?.Invoke() != false;
        }

        public static bool CanExecute<T, C>(this ICommand<T, C> command, C parameter)
        {
            command.AssertNotNull(nameof(command));

            return command.Handler != null && command.Condition?.Invoke(parameter) != false;
        }

        public static bool CanExecute<T>(this ICommand<T, T> command, T parameter)
        {
            command.AssertNotNull(nameof(command));

            return command.Handler != null && command.Condition?.Invoke(parameter) != false;
        }

        public static void Execute(this ICommand command)
        {
            command.AssertNotNull(nameof(command));

            if (command.Handler == null)
                throw HandlerNotSettedException();

            command.Handler();
        }

        public static void Execute<T>(this ICommand<T> command, T parameter)
        {
            command.AssertNotNull(nameof(command));

            if (command.Handler == null)
                throw HandlerNotSettedException();

            command.Handler(parameter);
        }

        #endregion

        #region Async

        public static bool CanExecute(this IAsyncCommand command)
        {
            command.AssertNotNull(nameof(command));

            return command.Handler != null && command.Condition?.Invoke() != false;
        }

        public static bool CanExecute<T>(this IAsyncCommand<T> command)
        {
            command.AssertNotNull(nameof(command));

            return command.Handler != null && command.Condition?.Invoke() != false;
        }

        public static bool CanExecute<T, C>(this IAsyncCommand<T, C> command, C condition)
        {
            command.AssertNotNull(nameof(command));

            return command.Handler != null && command.Condition?.Invoke(condition) != false;
        }

        public static bool CanExecute<T>(this IAsyncCommand<T, T> command, T parameter)
        {
            command.AssertNotNull(nameof(command));

            return command.Handler != null && command.Condition?.Invoke(parameter) != false;
        }

        public static Task ExecuteAsync(this IAsyncCommand command)
        {
            command.AssertNotNull(nameof(command));

            if (command.Handler == null)
                throw HandlerNotSettedException();

            return command.Handler();
        }

        public static Task ExecuteAsync<T>(this IAsyncCommand<T> command, T parameter)
        {
            command.AssertNotNull(nameof(command));

            if (command.Handler == null)
                throw HandlerNotSettedException();

            return command.Handler.Invoke(parameter);
        }

        public static Task ExecuteAsync<T, C>(this IAsyncCommand<T> command, T parameter)
        {
            command.AssertNotNull(nameof(command));

            if (command.Handler == null)
                throw HandlerNotSettedException();

            return command.Handler.Invoke(parameter);
        }

        #endregion

        private static InvalidOperationException HandlerNotSettedException() => new InvalidOperationException("Handler not setted");
    }
}
