using System.Threading.Tasks;

namespace Millarow.Threading
{
    public sealed class AsyncOperationLock<T> : AbstractAsyncOperationLock
    {
        protected override void OnReset()
        {
            Result = default(T);
            base.OnReset();
        }

        public void SetResult(T value)
        {
            Complete(() => Result = value);
        }

        public bool TrySetResult(T value)
        {
            return TryComplete(() => Result = value);
        }

        public T WaitResult()
        {
            Wait();

            return Result;
        }

        public Task<T> WaitResultAsync() => Task.Run(() => WaitResult());

        private T Result { get; set; }
    }
}