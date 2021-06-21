using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PublicationWebApi.Model;
using PublicationWebApi.Queries;

namespace PublicationWebApi.QueryHandlers
{
    public class GetPublicationsThatContainsTextQueryHandler : IRequestHandler<GetPublicationsThatContainsTextQuery, IEnumerable<PublicationViewModel>>
    {
        public async Task<IEnumerable<PublicationViewModel>> Handle(GetPublicationsThatContainsTextQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
