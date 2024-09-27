using Course.Web.Models.Discount;
using FluentValidation;

namespace Course.Web.Validators
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("İndirim alanı boş bırakılamaz!");
        }
    }
}
