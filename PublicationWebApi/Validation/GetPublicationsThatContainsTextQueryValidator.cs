using FluentValidation;
using PublicationWebApi.Queries;

namespace PublicationWebApi.Validation
{
    public class GetPublicationsThatContainsTextQueryValidator : AbstractValidator<GetPublicationsThatContainsTextQuery>
    {
        public GetPublicationsThatContainsTextQueryValidator()
        {
            RuleFor(x => x.Text).NotNull().WithMessage("Text cant be NULL");
        }
    }
}
