using fiap_fase1_tech_challenge.Modules.Users.Messages;
using FluentValidation;

namespace fiap_fase1_tech_challenge.Modules.Users.Validators
{
    public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateRequestValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage(UserMessages.Name.Required);

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage(UserMessages.Email.Required)
                .EmailAddress().WithMessage(UserMessages.Email.InvalidFormat);

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage(UserMessages.Password.Required)
                .MinimumLength(8).WithMessage(UserMessages.Password.InvalidLength)
                .Must(PasswordValidatorHelper.HasValidFormat).WithMessage(UserMessages.Password.InvalidFormat);

            RuleFor(u => u.RoleId)
                .NotEqual(0).WithMessage(UserMessages.Role.Required);
        }
    }
}
