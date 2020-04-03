//using Millarow.Rest.Core;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Millarow.Rest
//{
//    public class RequestDefaultsBuilder<TParent>
//        where TParent : class
//    {
//        private readonly IRestContainerBuilder _container;
//        private readonly TParent _parent;

//        internal RequestDefaultsBuilder(IRestContainerBuilder container, TParent parent)
//        {
//            _container = container;
//            _parent = parent;
//        }


//        public TParent DefaultEncoding(Encoding encoding)
//        {
//            encoding.AssertNotNull(nameof(encoding));

//            _defaultContentEncodingProvider.Encoding = encoding;

//            return _parent;
//        }

//        public TParent DefaultMediaType(MimeType mediaType)
//        {
//            mediaType.AssertNotNull(nameof(mediaType));

//            _defaultMediaTypeProvider.MediaType = mediaType;

//            return _parent;
//        }

//        public TParent WithDefaultRequestHeader(string name, string value)
//        {
//            name.AssertNotNullOrEmpty(nameof(name));

//            var header = new RequestHeader(name, RestValue.From(value));
//            _container.Register(new RequestEnricher(x => x.AddHeader(header)));

//            return _parent;
//        }

//        public TParent WithDefaultRequestQuery(string name, string value)
//        {
//            name.AssertNotNullOrEmpty(nameof(name));

//            var query = new RequestQuery(name, RestValue.From(value));
//            _container.Register(new RequestEnricher(x => x.AddQuery(query)));

//            return _parent;
//        }

//        public TParent WithDefaultRequestSegment(string name, string value)
//        {
//            name.AssertNotNullOrEmpty(nameof(name));

//            var segment = new RequestRouteSegment(name, RestValue.From(value));
//            _container.Register(new RequestEnricher(x => x.AddRouteSegment(segment)));

//            return _parent;
//        }

//        public TParent WithBearerAuthorization(string token)
//        {
//            return WithDefaultRequestHeader("Authorization", $"Bearer {token}");
//        }

//        public TParent WithBasicAuthorization(string userName, string password)
//        {
//            var bytes = Encoding.ASCII.GetBytes($"{userName}:{password}");

//            return WithDefaultRequestHeader("Authorization", $"Basic {Convert.ToBase64String(bytes)}");
//        }

//        private TParent With<T>(T component)
//            where T : class
//        {
//            component.AssertNotNull(nameof(component));

//            _container.Register(component);

//            return _parent;
//        }
//    }
//}
