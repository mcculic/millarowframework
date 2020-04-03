namespace Millarow.Rest.Core
{
    public interface IRestRequestEnricher
    {
        void Enrich(IRestRequest request);
    }
}
