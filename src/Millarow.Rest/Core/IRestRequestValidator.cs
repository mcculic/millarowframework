namespace Millarow.Rest.Core
{
    public interface IRestRequestValidator
    {
        void ValidateRequest(IRestRequest request);
    }
}
