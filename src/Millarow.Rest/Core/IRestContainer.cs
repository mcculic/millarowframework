using System.Collections.Generic;

namespace Millarow.Rest.Core
{
    public interface IRestContainer
    {
        IEnumerable<T> Resolve<T>();
    }
}