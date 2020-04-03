using System;
using System.Threading;

namespace Millarow.Presentation.MVP
{
    public static class BusyHelper
    {
        public static IDisposable BusyIn(this IBusy busy, string message = null)
        {
            busy.AssertNotNull(nameof(busy));

            if (busy.BusyState.IsBusy)
                throw Exceptions.ReentrancyCheckFailed();

            busy.BusyState.Message = message;
            busy.BusyState.IsBusy = true;

            return new BusyScope(busy.BusyState);
        }

        public static void AllowCancel(this IBusyState busyState, CancellationTokenSource cancellationTokenSource, string cancelCaption)
        {
            busyState.AssertNotNull(nameof(busyState));
            cancellationTokenSource.AssertNotNull(nameof(cancellationTokenSource));

            busyState.CancellationCallback = () => cancellationTokenSource.Cancel();
            busyState.CancelCaption = cancelCaption;
            busyState.CanCancel = true;
        }

        public class BusyScope : IDisposable
        {
            public BusyScope(IBusyState busyState)
            {
                busyState.AssertNotNull(nameof(busyState));

                BusyState = busyState;
            }

            public void Dispose()
            {
                BusyState.Reset();
            }

            protected IBusyState BusyState { get; }
        }
    }
}
