namespace Millarow.Rest.Attributes
{
    public sealed class FormUrlEncodedRequestAttribute : RequestAttribute
    {
        public override RequestBodyType Kind => RequestBodyType.Auto;
    }
}
