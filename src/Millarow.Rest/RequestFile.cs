namespace Millarow.Rest
{
    public class RequestFile
    {
        public RequestFile(RequestContent content, string name)
        {
            content.AssertNotNull(nameof(content));

            Content = content;
            Name = name;
        }

        public RequestContent Content { get; }

        public string Name { get; }

        public string FileName { get; set; }
    }
}
