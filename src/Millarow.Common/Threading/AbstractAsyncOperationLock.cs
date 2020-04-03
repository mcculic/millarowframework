using System;
using System.Threading;

namespace Millarow.Threading
{
    public abstract class AbstractAsyncOperationLock : IDisposable
    {
        public AbstractAsyncOperationLock()
        {
            WaitHandle = new ManualResetEvent(false);
        }

        public void SetException(Exception exception)
        {
            Complete(() => Exception = exception);
        }

        public bool TrySetException(Exception exception)
        {
            return TryComplete(() => Exception = exception);
        }

        public void Reset()
        {
            lock (SyncRoot)
            {
                OnReset();
                IsCompleted = false;
                WaitHandle.Reset();
            }
        }

        protected virtual void OnReset()
        {
            Exception = null;
        }

        protected void Wait()
        {
            WaitHandle.WaitOne();

            if (Exception != null)
                throw Exception;
        }

        protected bool TryComplete(Action trailer = null)
        {
            lock (SyncRoot)
            {
                if (IsCompleted)
                    return false;

                trailer?.Invoke();

                IsCompleted = true;
                WaitHandle.Set();
            }

            return true;
        }

        protected void Complete(Action trailer = null)
        {
            if (!TryComplete(trailer))
                throw new InvalidOperationException("An attempt was made to transition to a final state when it had already completed.");
        }

        protected void CheckDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().ToString());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (disposing)
            {
                WaitHandle.Dispose();
            }

            IsDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~AbstractAsyncOperationLock()
        {
            Dispose(false);
        }

        private ManualResetEvent WaitHandle { get; }

        private bool IsCompleted { get; set; }

        private Exception Exception { get; set; }

        private bool IsDisposed { get; set; }

        private object SyncRoot => WaitHandle;
    }
}
