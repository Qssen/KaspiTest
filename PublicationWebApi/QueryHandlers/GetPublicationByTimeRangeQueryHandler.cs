using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PublicationWebApi.Model;
using PublicationWebApi.Queries;

namespace PublicationWebApi.QueryHandlers
{
    public class GetPublicationByTimeRangeQueryHandler : IRequestHandler<GetPublicationsByTimeRangeQuery, IEnumerable<PublicationViewModel>>
    {
        public async Task<IEnumerable<PublicationViewModel>> Handle(GetPublicationsByTimeRangeQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
