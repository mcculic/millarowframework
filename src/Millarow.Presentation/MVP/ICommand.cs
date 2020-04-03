using System;

namespace Millarow.Presentation.MVP
{
    public interface ICommand : IConditional
    {
        Action Handler { get; set; }
    }
}
