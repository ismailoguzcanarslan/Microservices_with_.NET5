using FluentValidation;
using FreeCourse.Web.Models;

namespace FreeCourse.Web.Validators
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(a => a.Code).NotEmpty().WithMessage("Code can not be null");
        }
    }
}
