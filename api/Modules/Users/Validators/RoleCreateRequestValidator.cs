using fiap_fase1_tech_challenge.Modules.Users.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Users.Messages;
using FluentValidation;

namespace fiap_fase1_tech_challenge.Modules.Users.Validators
{
    public class RoleCreateRequestValidator : AbstractValidator<RoleCreateRequest>
    {
        public RoleCreateRequestValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage(RoleMessages.Name.Required);
        }
    }
}
