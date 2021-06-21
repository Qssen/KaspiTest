using System;
using System.Collections.Generic;
using MediatR;
using PublicationWebApi.Model;

namespace PublicationWebApi.Queries
{
    public class GetPublicationsByTimeRangeQuery : IRequest<IEnumerable<PublicationViewModel>>
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
