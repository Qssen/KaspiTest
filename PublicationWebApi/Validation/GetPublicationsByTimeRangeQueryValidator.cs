using FluentValidation;
using PublicationWebApi.Queries;

namespace PublicationWebApi.Validation
{
    public class GetPublicationsByTimeRangeQueryValidator : AbstractValidator<GetPublicationsByTimeRangeQuery>
    {

        public GetPublicationsByTimeRangeQueryValidator()
        {
            RuleFor(x => x.DateFrom)
                .NotNull().When(x => x.DateTo == null);
            
            RuleFor(x => x.DateTo)
                .NotNull().When(x => x.DateFrom == null);

            RuleFor(x => x.DateTo).GreaterThan(x => x.DateFrom)
                .When(x => x.DateFrom.HasValue && x.DateTo.HasValue).WithMessage("Date To cant be less than Date From");
        }
    }
}
