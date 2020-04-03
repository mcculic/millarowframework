namespace Millarow.Rest.ModelBinding.Binders
{
    public class DefaultResponseBinder : IResponseBinder
    {
        public void Bind(IResponseBindingContext bindingContext)
        {
            bindingContext.AssertNotNull(nameof(bindingContext));
        }
    }
}
