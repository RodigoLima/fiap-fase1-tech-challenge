﻿using fiap_fase1_tech_challenge.Modules.Promotions.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Promotions.Messages;
using FluentValidation;

namespace fiap_fase1_tech_challenge.Modules.Promotions.Validators
{
    public class PromotionCreateRequestValidator : AbstractValidator<PromotionCreateRequest>
    {
        public PromotionCreateRequestValidator()
        {
            RuleFor(p => p.DiscountPercentage)
                .GreaterThan(0).WithMessage(PromotionMessages.Discount.GreatherThan)
                .LessThan(101).WithMessage(PromotionMessages.Discount.LessThan);

            RuleFor(p => p.InitialDate)
                .GreaterThanOrEqualTo(_ => DateTime.Today)
                .WithMessage(PromotionMessages.InitialDate.GreaterOrEqual);

            RuleFor(p => p.FinalDate)
                .GreaterThan(p => p.InitialDate)
                .WithMessage(PromotionMessages.FinalDate.GreaterThan);

            RuleFor(p => p.GameId)
                .GreaterThan(0).WithMessage(PromotionMessages.Game.Required);
        }
    }
}
