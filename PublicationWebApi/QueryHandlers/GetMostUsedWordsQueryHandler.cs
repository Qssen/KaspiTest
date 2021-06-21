using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PublicationWebApi.Model;
using PublicationWebApi.Queries;

namespace PublicationWebApi.QueryHandlers
{
    public class GetMostUsedWordsQueryHandler : IRequestHandler<GetMostUsedWordsQuery, IEnumerable<GetMostUsedWordsQueryResponseModel>>
    {
        public async Task<IEnumerable<GetMostUsedWordsQueryResponseModel>> Handle(GetMostUsedWordsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
