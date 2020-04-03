using System.Globalization;

namespace Millarow.Rest.Core
{
    public interface IDefaultCultureProvider
    {
        CultureInfo Culture { get; }
    }
}
