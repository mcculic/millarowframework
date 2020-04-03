namespace Millarow.Rest
{
    public static class MimeTypes
    {
        public static class Text
        {
            public const string All = "text/*";

            public const string Plain = "text/plain";
            public const string Html = "text/html";
            public const string Xml = "text/xml";
            public const string Rtf = "text/richtext";
        }

        public static class Application
        {
            public const string Json = "application/json";
            public const string OctetStream = "application/octet-stream";
            public const string FormUrlEncoded = "application/x-www-form-urlencoded";
        }

        public static class Multipart
        {
            public const string FormData = "multipart/form-data";
        }

        public static class Image
        {
            public const string Gif = "image/gif";
            public const string Jpeg = "image/jpeg";
            public const string Pjpeg = "image/pjpeg";
            public const string Png = "image/png";
            public const string Svg = "image/svg+xml";
            public const string Tiff = "image/tiff";
            public const string Ico = "image/vnd.microsoft.icon";
            public const string Wbmp = "image/vnd.wap.wbmp";
            public const string Webp = "image/webp";
        }
    }
}
