using System;

namespace Millarow.Presentation.MVP
{
    public interface ICommand<T> : IConditional
    {
        Action<T> Handler { get; set; }
    }
}
