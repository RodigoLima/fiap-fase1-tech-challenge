using fiap_fase1_tech_challenge.Messages;
using fiap_fase1_tech_challenge.Validators.User;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace fiap_fase1_tech_challenge.Test.UnitTests.Validators.User
{
    public class UserUpdateRequestValidatorTest
    {
        private readonly UserUpdateRequestValidator _validator;

        public UserUpdateRequestValidatorTest()
        {
            _validator = new UserUpdateRequestValidator();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnNotEmptyFieldForName")]
        public void Validate_ShouldReturnNotEmptyFieldForName()
        {
            //Arrange
            var request = new UserUpdateRequest
            {
                Name = "",
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.Name).WithErrorMessage(UserMessages.Name.CannotBeEmpty);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnNotEmptyFieldForEmail")]
        public void Validate_ShouldReturnNotEmptyFieldForEmail()
        {
            //Arrange
            var request = new UserUpdateRequest
            {
                Email = "",
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.Email).WithErrorMessage(UserMessages.Email.CannotBeEmpty);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnInvalidFormatForEmail")]
        public void Validate_ShouldReturnInvalidFormatForEmail()
        {
            //Arrange
            var request = new UserUpdateRequest
            {
                Email = "invalidMail",
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.Email).WithErrorMessage(UserMessages.Email.InvalidFormat);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnNotEmptyFieldForOldPassword")]
        public void Validate_ShouldReturnNotEmptyFieldForOldPassword()
        {
            //Arrange
            var request = new UserUpdateRequest
            {
                OldPassword = "",
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.OldPassword).WithErrorMessage(UserMessages.Password.CannotBeEmptyOld);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnRequiredFieldForOldPassword")]
        public void Validate_ShouldReturnRequiredFieldForOldPassword()
        {
            //Arrange
            var request = new UserUpdateRequest
            {
                NewPassword = "@Password1234",
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.OldPassword).WithErrorMessage(UserMessages.Password.RequiredOld);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnNotEmptyFieldForNewPassword")]
        public void Validate_ShouldReturnNotEmptyFieldForNewPassword()
        {
            //Arrange
            var request = new UserUpdateRequest
            {
                NewPassword = "",
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.NewPassword).WithErrorMessage(UserMessages.Password.CannotBeEmpty);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnInvalidSizeForNewPassword")]
        public void Validate_ShouldReturnInvalidSizeForNewPassword()
        {
            //Arrange
            var request = new UserUpdateRequest
            {
                NewPassword = "@Pa123",
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.NewPassword).WithErrorMessage(UserMessages.Password.InvalidLength);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnInvalidFormatForNewPassword")]
        public void Validate_ShouldReturnInvalidFormatForNewPassword()
        {
            //Arrange
            var request = new UserUpdateRequest
            {
                NewPassword = "password",
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.NewPassword).WithErrorMessage(UserMessages.Password.InvalidFormat);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnInvalidRole")]
        public void Validate_ShouldReturnInvalidRole()
        {
            //Arrange
            var request = new UserUpdateRequest
            {
                RoleId = 0,
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.RoleId).WithErrorMessage(UserMessages.Role.Invalid);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnMoreThanOneError")]
        public void Validate_ShouldReturnMoreThanOneError()
        {
            //Arrange
            var request = new UserUpdateRequest
            {
                Email = "invalidMail",
                RoleId = 0,
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrors();
            result.Errors.Count().Should().BeGreaterThan(1);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserUpdateRequestValidator")]
        [Theory(DisplayName = "Validate_ShouldPass_WhenOnlyOneFieldIsProvided")]
        [InlineData("newName", null, null, null)]
        [InlineData(null, "valid@mail.com", null, null)]
        [InlineData(null, null, "@Password123", null)]
        [InlineData(null, null, null, 1)]
        public void Validate_ShouldPassTheValidation(string? name, string? email, string? password, int? roleId)
        {
            //Arrange
            var request = new UserUpdateRequest
            {
                Name = name,
                Email = email,
                OldPassword = password,
                NewPassword = password,
                RoleId = roleId
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
