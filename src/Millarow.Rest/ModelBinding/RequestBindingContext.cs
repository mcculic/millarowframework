using Millarow.Rest.Metadata;
using Millarow.Rest.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Millarow.Rest.ModelBinding
{
    public class RequestBindingContext : IRequestBindingContext
    {
        private readonly List<RequestHeader> _headers = new List<RequestHeader>();
        private readonly List<RequestRouteSegment> _path = new List<RequestRouteSegment>();
        private readonly List<RequestQuery> _queries = new List<RequestQuery>();
        private readonly List<RequestFormField> _formFields = new List<RequestFormField>();
        private readonly List<RequestFile> _formFiles = new List<RequestFile>();

        public RequestBindingContext(RequestMetadata metadata, IBindingData bindingSource)
        {
            metadata.AssertNotNull(nameof(metadata));
            bindingSource.AssertNotNull(nameof(bindingSource));

            Metadata = metadata;
            BindingSource = bindingSource;

            Timeout = metadata.Timeout;
            Route = metadata.Route;
        }

        public void AddHeader(RequestHeader header)
        {
            header.AssertNotNull(nameof(header));

            _headers.Add(header);
        }

        public void AddRouteSegment(RequestRouteSegment segment)
        {
            segment.AssertNotNull(nameof(segment));

            _path.Add(segment);
        }

        public void AddQuery(RequestQuery query)
        {
            query.AssertNotNull(nameof(query));

            _queries.Add(query);
        }


        public void AddFormField(RequestFormField field)
        {
            field.AssertNotNull(nameof(field));

            _formFields.Add(field);
        }

        public void AddFormFile(RequestFile file)
        {
            file.AssertNotNull(nameof(file));

            _formFiles.Add(file);
        }

        public RequestMetadata Metadata { get; }

        public IBindingData BindingSource { get; }

        public string Route { get; set; }

        public TimeSpan? Timeout { get; set; }

        public CultureInfo CultureInfo { get; set; }
      
        public RequestBody Body { get; set; }

        public IEnumerable<RequestHeader> Headers => _headers;

        public IEnumerable<RequestRouteSegment> Path => _path;

        public IEnumerable<RequestQuery> Queries => _queries;

        public IEnumerable<RequestFormField> FormFields => _formFields;

        public IEnumerable<RequestFile> FormFiles => _formFiles;
    }
}
