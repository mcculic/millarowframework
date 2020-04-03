using System.Threading.Tasks;

namespace Millarow.Threading
{
    public sealed class AsyncOperationLock : AbstractAsyncOperationLock
    {
        public void SetCompleted()
        {
            Complete();
        }

        public bool TrySetCompleted()
        {
            return TryComplete();
        }

        public void WaitComplete()
        {
            Wait();
        }

        public Task WaitCompleteAsync() => Task.Run(() => WaitComplete());
    }
}