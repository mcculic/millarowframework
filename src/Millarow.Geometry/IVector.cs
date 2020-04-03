using System;

namespace Millarow.Geometry
{
    public interface IVector<T>
        where T : struct, IFormattable
    {
        T X { get; }

        T Y { get; }
    }
}