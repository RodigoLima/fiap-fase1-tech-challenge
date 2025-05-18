using fiap_fase1_tech_challenge.Modules.Users.Messages;
using FluentValidation;

namespace fiap_fase1_tech_challenge.Modules.Users.Validators
{
    public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
    {
        public UserUpdateRequestValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage(UserMessages.Name.CannotBeEmpty)
                .When(u => u.Name != null);

            RuleFor(u => u.Email)
                .EmailAddress().WithMessage(UserMessages.Email.InvalidFormat)
                .When(u => !string.IsNullOrWhiteSpace(u.Email), ApplyConditionTo.CurrentValidator)
                .NotEmpty().WithMessage(UserMessages.Email.CannotBeEmpty)
                .When(u => u.Email != null, ApplyConditionTo.CurrentValidator);


            RuleFor(u => u.OldPassword)
                .NotNull().WithMessage(UserMessages.Password.RequiredOld)
                .When(u => !string.IsNullOrWhiteSpace(u.NewPassword), ApplyConditionTo.CurrentValidator)
                .NotEmpty().WithMessage(UserMessages.Password.CannotBeEmptyOld)
                .When(u => u.OldPassword != null, ApplyConditionTo.CurrentValidator);


            RuleFor(u => u.NewPassword)
                .NotEmpty().WithMessage(UserMessages.Password.CannotBeEmpty)
                .When(u => u.NewPassword != null, ApplyConditionTo.CurrentValidator)
                .MinimumLength(8).WithMessage(UserMessages.Password.InvalidLength)
                .When(u => !string.IsNullOrWhiteSpace(u.NewPassword), ApplyConditionTo.CurrentValidator)
                .Must(PasswordValidatorHelper.HasValidFormat!).WithMessage(UserMessages.Password.InvalidFormat)
                .When(u => !string.IsNullOrWhiteSpace(u.NewPassword) && u.NewPassword.Count() >= 8, ApplyConditionTo.CurrentValidator);

            RuleFor(u => u.RoleId)
                .GreaterThan(0).WithMessage(UserMessages.Role.Invalid)
                .When(u => u.RoleId != null);
        }
    }
}
