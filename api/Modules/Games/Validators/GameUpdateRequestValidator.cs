using fiap_fase1_tech_challenge.Modules.Games.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Games.Messages;
using FluentValidation;

namespace fiap_fase1_tech_challenge.Modules.Games.Validators
{
    public class GameUpdateRequestValidator : AbstractValidator<GameUpdateRequest>
    {
        public GameUpdateRequestValidator()
        {
            RuleFor(g => g.Name)
                .MaximumLength(50).WithMessage(GameMessages.Name.MaxLength);

            RuleFor(g => g.Description)
                .MaximumLength(500).WithMessage(GameMessages.Description.MaxLength);

            RuleFor(g => g.Price)
                .GreaterThan(0).WithMessage(GameMessages.Price.GreaterThanZero)
                .When(g => g.Price != null);
        }
    }
}
