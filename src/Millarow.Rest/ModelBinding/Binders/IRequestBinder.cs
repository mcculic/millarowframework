namespace Millarow.Rest.ModelBinding.Binders
{
    public interface IRequestBinder
    {
        void Bind(IRequestBindingContext bindingContext);
    }
}
