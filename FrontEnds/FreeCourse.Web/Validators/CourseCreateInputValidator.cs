using FluentValidation;
using FreeCourse.Web.Models;

namespace FreeCourse.Web.Validators
{
    public class CourseCreateInputValidator : AbstractValidator<CourseCreateInput>
    {
        public CourseCreateInputValidator()
        {
            RuleFor(a=>a.Name).NotEmpty().WithMessage("Name can not be null");
            RuleFor(a => a.Description).NotEmpty().WithMessage("Description can not be null");
            RuleFor(a => a.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("Duration can not be null");
            RuleFor(a => a.Price).NotEmpty().ScalePrecision(2,6).WithMessage("Price can not be null");
            RuleFor(a => a.CategoryId).NotEmpty().WithMessage("Category can not be null");
        }
    }
}
