using System;

namespace Millarow.Presentation.MVP
{
    public interface IConditional<T>
    {
        void InvalidateCondition();

        Func<T, bool> Condition { get; set; }
    }
}