namespace Millarow.Rest.Core
{
    public interface ICompositeComponent
    {
        void ResolveDependencies(IRestContainer container);
    }
}
