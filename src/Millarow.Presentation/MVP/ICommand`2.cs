using System;

namespace Millarow.Presentation.MVP
{
    public interface ICommand<T, C> : IConditional<C>
    {
        Action<T> Handler { get; set; }
    }
}
