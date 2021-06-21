using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PublicationWebApi.Queries;

namespace PublicationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PublicationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<object> GetPublications()
        {
            return await _mediator.Send(new GetPublicationsByTimeRangeQuery());
        }
    }
}
