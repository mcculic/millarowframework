using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP
{
    public interface IAsyncCommand : IConditional
    {
        Func<Task> Handler { get; set; }
    }
}
