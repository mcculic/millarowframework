using Millarow.Rest.Core;

namespace Millarow.Rest.ModelBinding.Binders
{
    public interface IBindingData
    {
        bool TryGetValue(string sourceName, BindingPath bindingPath, out RestValue value);
    }
}
