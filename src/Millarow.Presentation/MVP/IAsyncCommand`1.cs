using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP
{
    public interface IAsyncCommand<T> : IConditional
    {
        Func<T, Task> Handler { get; set; }
    }
}