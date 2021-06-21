using System.Collections.Generic;
using MediatR;
using PublicationWebApi.Model;

namespace PublicationWebApi.Queries
{
    public class GetMostUsedWordsQuery : IRequest<IEnumerable<GetMostUsedWordsQueryResponseModel>>
    {
        public int Top { get; set; } = 10;
    }
}
