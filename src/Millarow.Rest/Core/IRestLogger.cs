using System;

namespace Millarow.Rest.Core
{
    public interface IRestLogger
    {
        void LogInfo(string message);

        void LogError(string message, Exception exception);
    }
}
