using fiap_fase1_tech_challenge.Modules.Users.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Users.Messages;
using FluentValidation;

namespace fiap_fase1_tech_challenge.Modules.Users.Validators
{
    public class RoleUpdateRequestValidator : AbstractValidator<RoleUpdateRequest>
    {
        public RoleUpdateRequestValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage(RoleMessages.Name.CannotBeEmpty)
                .When(r => r.Name != null);
        }
    }
}
