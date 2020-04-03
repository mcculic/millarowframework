using Millarow.Rest.Core;
using Millarow.Rest.Metadata.Providers;
using Millarow.Rest.Serialization;
using System;

namespace Millarow.Rest.Metadata
{
    public static class MetadataValueProviderHelper
    {
        public static bool TryGetParameterKind(this MetadataValueProvider valueProvider, out RequestParameterKind result)
            => valueProvider.TryGetValue<IParameterDefinition, RequestParameterKind>(x => x.ParameterKind, out result);

        public static bool TryGetRequestMethod(this MetadataValueProvider valueProvider, out RequestMethod result)
            => valueProvider.TryGetValue<IRequestMethodProvider, RequestMethod>(x => x.RequestMethod, out result);

        public static string GetRequestRoutePrefix(this MetadataValueProvider valueProvider)
            => valueProvider.GetValue<IRequestRoutePrefixProvider, string>(x => x.RoutePrefix);

        public static string GetRequestRoute(this MetadataValueProvider valueProvider)
            => valueProvider.GetValue<IRequestRouteProvider, string>(x => x.Route);

        public static BindingPath GetBindingPath(this MetadataValueProvider valueProvider)
            => valueProvider.GetValue<IBindingPathProvider, BindingPath>(x => x.BindingPath) ?? BindingPath.Empty;

        public static TimeSpan? GetRequestTimeout(this MetadataValueProvider valueProvider)
            => valueProvider.GetNullable<ITimeoutProvider, TimeSpan>(x => x.Timeout);

        public static string GetParameterName(this MetadataValueProvider valueProvider)
            => valueProvider.GetValue<IParameterNameProvider, string>(x => x.Name);

        public static bool GetIsRequired(this MetadataValueProvider valueProvider)
            => valueProvider.GetValue<IIsRequiredProvider, bool>(x => x.IsRequired, false);

        public static MimeType GetContentMediaType(this MetadataValueProvider valueProvider)
            => valueProvider.GetValue<IContentMediaTypeProvider, MimeType>(x => x.MediaType);

        public static string GetContentCharSet(this MetadataValueProvider valueProvider)
            => valueProvider.GetValue<IContentCharSetProvider, string>(x => x.CharSet);

        public static string GetContentFileName(this MetadataValueProvider valueProvider)
            => valueProvider.GetValue<IContentFileNameProvider, string>(x => x.FileName);

        public static string GetStringFormat(this MetadataValueProvider valueProvider)
            => valueProvider.GetValue<IStringFormatProvider, string>(x => x.Format);

        public static string GetParameterPrefix(this MetadataValueProvider valueProvider)
            => valueProvider.GetValue<IRestNamePrefixProvider, string>(x => x.Prefix);

        public static RequestBodyType GetRequestContentKind(this MetadataValueProvider valueProvider)
            => valueProvider.GetValue<IRequestContentKindProvider, RequestBodyType>(x => x.GetKind(), RequestBodyType.Auto);
    }
}
