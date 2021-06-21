using System.Collections.Generic;
using MediatR;
using PublicationWebApi.Model;

namespace PublicationWebApi.Queries
{
    public class GetPublicationsThatContainsTextQuery : IRequest<IEnumerable<PublicationViewModel>>
    {
        public string Text { get; set; }
    }
}
