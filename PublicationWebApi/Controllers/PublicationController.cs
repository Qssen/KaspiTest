using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicationWebApi.Model;
using PublicationWebApi.Queries;
using PublicationWebApi.Validation;

namespace PublicationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private readonly IMediator _mediator;

        //TODO: Generic validator
        private readonly GetPublicationsByTimeRangeQueryValidator _publicationByTimeRangeQueryValidator;
        private readonly GetPublicationsThatContainsTextQueryValidator _getPublicationsThatContainsTextQueryValidator;

        public PublicationController(IMediator mediator, 
            GetPublicationsByTimeRangeQueryValidator publicationByTimeRangeQueryValidator, 
            GetPublicationsThatContainsTextQueryValidator getPublicationsThatContainsTextQueryValidator)
        {
            _mediator = mediator;
            _publicationByTimeRangeQueryValidator = publicationByTimeRangeQueryValidator;
            _getPublicationsThatContainsTextQueryValidator = getPublicationsThatContainsTextQueryValidator;
        }

        //[CustomAuth]
        [HttpGet("/api/posts")]
        public async Task<IEnumerable<PublicationViewModel>> GetPublications([FromQuery] GetPublicationsByTimeRangeQuery model)
        {
            var validationResult = await _publicationByTimeRangeQueryValidator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await Response.WriteAsync(validationResult.ToString());
                return null;
            }

            return await _mediator.Send(model);
        }

        //[CustomAuth]
        [HttpGet("/api/topWords")]
        public async Task<IEnumerable<GetMostUsedWordsQueryResponseModel>> GetMostUsedWords([FromQuery] GetMostUsedWordsQuery model) 
            => await _mediator.Send(model);

        //[CustomAuth]
        [HttpGet("/api/search")]
        public async Task<IEnumerable<PublicationViewModel>> GetPublicationsByText(
            [FromQuery] GetPublicationsThatContainsTextQuery model)
        {
            var validationResult = await _getPublicationsThatContainsTextQueryValidator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await Response.WriteAsync(validationResult.ToString());
                return null;
            }

            return await _mediator.Send(model);
        }

    }
}
