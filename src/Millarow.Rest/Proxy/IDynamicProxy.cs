namespace Millarow.Rest.Core
{
    public interface IDynamicProxy
    {
        object Invoke(string methodSignature, object[] args);
    }
}
