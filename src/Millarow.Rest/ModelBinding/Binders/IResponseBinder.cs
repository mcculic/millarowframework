namespace Millarow.Rest.ModelBinding.Binders
{
    public interface IResponseBinder
    {
        void Bind(IResponseBindingContext bindingContext);
    }
}
