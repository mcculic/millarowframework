using System;

namespace Millarow.Presentation.MVP
{
    public interface IConditional
    {
        void InvalidateCondition();

        Func<bool> Condition { get; set; }
    }
}
