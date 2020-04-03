using Millarow.Rest.Core;
using Millarow.Rest.Http;
using Millarow.Rest.Metadata;
using Millarow.Rest.ModelBinding.Binders;
using Millarow.Rest.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Millarow.Rest.ModelBinding
{
    public class RestModelMapper : ICompositeComponent
    {
        public void ResolveDependencies(IRestContainer container)
        {
            RequestBinders = container.Resolve<IRequestBinder>().ToArray();
            ResponseBinders = container.Resolve<IResponseBinder>().ToArray();
            ContentMapper = container.Required<DefaultHttpRequestContentFormatter>();
            RouteRenderer = container.Required<RestRouteRenderer>();
            RestSerializer = container.Required<RestSerializer>();
            DefaultCultureInfoProvider = container.Required<IDefaultCultureProvider>();
        }

        public IRestRequest MapRequest(RequestMetadata metadata, IBindingData bindingSource)
        {
            metadata.AssertNotNull(nameof(metadata));
            bindingSource.AssertNotNull(nameof(bindingSource));

            var bindingContext = new RequestBindingContext(metadata, bindingSource);

            foreach (var binder in RequestBinders)
                binder.Bind(bindingContext);

            //TODO validate
            //bindingContext.Validate();

            if (bindingContext.FormFiles.Any())
            {
                throw new NotImplementedException();//TODO !
            }

            var request = new RestRequest()
            {
                Method = bindingContext.Metadata.Method,
                Route = bindingContext.Route,
                Timeout = bindingContext.Timeout
            };

            //TODO !!!
            if (bindingContext.FormFields.Any())
            {
                var content = new RequestUrlEncodedContent("utf-8"); //TODO

                foreach (var field in bindingContext.FormFields)
                    content.AddField(field);

                request.Content = content;
            }
            else if (bindingContext.Body != null)
            {
                request.Content = bindingContext.Body;
            }

            foreach (var header in bindingContext.Headers)
                request.AddHeader(header);

            foreach (var segment in bindingContext.Path)
                request.AddRouteSegment(segment);

            foreach (var query in bindingContext.Queries)
                request.AddQuery(query);

            return request;
        }


        public object MapResponse(ResponseMetadata metadata, IResponse response)
        {
            response.AssertNotNull(nameof(response));

            if (metadata.Parameters.Count == 1)
            {
                var p = metadata.Parameters[0];
                if (p.ParameterKind == RequestParameterKind.Body)
                {
                    var content = response.Content;
                    var contentData = content.ReadAsArray();

                    return RestSerializer.DeserializeContent(contentData, content.ContentType, metadata.ResultType);
                }
            }

            var model = Activator.CreateInstance(metadata.ResultType); //TODO
            var bindingContext = new ResponseBindingContext(metadata, model, response);

            foreach (var p in metadata.Parameters)
            {
                //if (p.ParameterKind == RequestParameterKind.Content)
                //{
                //    if (p.ContentKind == RestContentKind.File)
                //    {

                //    }
                //    else
                //    {
                //        throw new NotImplementedException("TODO");
                //    }
                //}
                //else
                {
                    throw new NotImplementedException("TODO");
                }
            }

            return model;
        }

        //private RequestBodyType GetBodyType(RequestBindingContext bindingContext)
        //{
        //    var contents = new List<RequestContent>();

        //    if (bindingContext.Body != null)
        //        contents.Add(bindingContext.Body);

        //    contents.AddRange(bindingContext.FormFiles);
        //}

        protected IReadOnlyCollection<IRequestBinder> RequestBinders { get; private set; }

        protected IReadOnlyCollection<IResponseBinder> ResponseBinders { get; private set; }

        protected DefaultHttpRequestContentFormatter ContentMapper { get; private set; }

        protected RestRouteRenderer RouteRenderer { get; private set; }

        protected IDefaultCultureProvider DefaultCultureInfoProvider { get; private set; }

        protected RestSerializer RestSerializer { get; private set; }
    }
}