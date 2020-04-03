using System;
using System.Collections.Generic;

namespace Millarow.Rest
{
    public interface IResponse
    {
        object ReadContentAs(Type type);
        
        T ReadContentAs<T>();
        
        IRestRequest Request { get; }

        int StatusCode { get; }
      
        string StatusDescription { get; }

        IEnumerable<ResponseHeader> Headers { get; }

        ResponseContent Content { get; }
    }
}
