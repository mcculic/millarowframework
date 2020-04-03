namespace Millarow.Rest.Core
{
    public interface IRestContainerBuilder
    {
        void Register(object component);

        void Register<T>() where T : new();

        IRestContainer CreateContainer();
    }
}