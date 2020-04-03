using System;

namespace Millarow.Presentation.WPF.Input
{
    [Flags]
    public enum RectManipulations
    {
        None = 0,
        Move = 1,
        ResizeL = 2,
        ResizeR = 4,
        ResizeT = 8,
        ResizeB = 16
    }
}