using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP
{
    public interface IAsyncCommand<T, C> : IConditional<C>
    {
        Func<T, Task> Handler { get; set; }
    }
}