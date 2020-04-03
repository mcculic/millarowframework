namespace Millarow.Rest.Attributes
{
    public sealed class MultipartRequestAttribute : RequestAttribute
    {
        public override RequestBodyType Kind => RequestBodyType.Multipart;

        public string Boundary { get; set; }
    }
}
