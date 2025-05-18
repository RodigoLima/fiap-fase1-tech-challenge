using fiap_fase1_tech_challenge.Modules.Promotions.Messages;
using FluentValidation;

namespace fiap_fase1_tech_challenge.Modules.Promotions.Validators
{
    public class PromotionUpdateRequestValidator : AbstractValidator<PromotionUpdateRequest>
    {
        public PromotionUpdateRequestValidator()
        {
            When(p => p.DiscountPercentage != null, () =>
            {
                RuleFor(p => p.DiscountPercentage)
                    .GreaterThan(0).WithMessage(PromotionMessages.Discount.GreatherThan)
                    .LessThan(101).WithMessage(PromotionMessages.Discount.LessThan);
            });

            RuleFor(p => p.InitialDate)
                .GreaterThanOrEqualTo(_ => DateTime.Today).WithMessage(PromotionMessages.InitialDate.GreaterOrEqual)
                .When(p => p.InitialDate != null, ApplyConditionTo.CurrentValidator)
                .NotNull().WithMessage(PromotionMessages.InitialDate.Required)
                .When(p => p.FinalDate != null, ApplyConditionTo.CurrentValidator);

            RuleFor(p => p.FinalDate)
                .GreaterThanOrEqualTo(p => p.InitialDate).WithMessage(PromotionMessages.FinalDate.GreaterThan)
                .When(p => p.FinalDate != null, ApplyConditionTo.CurrentValidator)
                .NotNull().WithMessage(PromotionMessages.FinalDate.Required)
                .When(p => p.InitialDate != null, ApplyConditionTo.CurrentValidator);

            RuleFor(p => p.GameId)
                .GreaterThan(0).WithMessage(PromotionMessages.Game.Required)
                .When(p => p.GameId != null);
        }
    }
}
