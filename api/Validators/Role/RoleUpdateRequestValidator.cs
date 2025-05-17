using fiap_fase1_tech_challenge.DTOs.Role;
using fiap_fase1_tech_challenge.Messages;
using FluentValidation;

namespace fiap_fase1_tech_challenge.Validators.Role
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
