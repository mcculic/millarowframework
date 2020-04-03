namespace Millarow.Rest.Attributes
{
    public sealed class SimpleRequestAttribute : RequestAttribute
    {
        public override RequestBodyType Kind => RequestBodyType.Single;
    }
}
