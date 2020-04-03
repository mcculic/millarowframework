using Millarow.Rest.Metadata;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Millarow.Rest.ModelBinding.Binders
{
    public interface IRequestBindingContext
    {
        void AddHeader(RequestHeader header);

        void AddRouteSegment(RequestRouteSegment segment);

        void AddQuery(RequestQuery query);

        void AddFormField(RequestFormField field);

        void AddFormFile(RequestFile file);

        RequestMetadata Metadata { get; }

        IBindingData BindingSource { get; }

        CultureInfo CultureInfo { get; }

        TimeSpan? Timeout { get; set; }

        RequestBody Body { get; set; }

        IEnumerable<RequestHeader> Headers { get; }

        IEnumerable<RequestRouteSegment> Path { get; }

        IEnumerable<RequestQuery> Queries { get; }
        
        IEnumerable<RequestFormField> FormFields { get; }
        
        IEnumerable<RequestFile> FormFiles { get; }
    }
}