using fiap_fase1_tech_challenge.Modules.Users.Messages;
using fiap_fase1_tech_challenge.Modules.Users.Validators;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace fiap_fase1_tech_challenge.Test.UnitTests.Validators.User
{
    public class UserCreateRequestValidatorTest
    {
        private readonly UserCreateRequestValidator _validator;
        public UserCreateRequestValidatorTest()
        {
            _validator = new UserCreateRequestValidator();    
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnRequiredFieldForName")]
        public void Validate_ShouldReturnRequiredFieldForName()
        {
            //Arrange
            var request = new UserCreateRequest
            {
                Email = "mail@test.com",
                Password = "@Password1234",
                RoleId = 1
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.Name).WithErrorMessage(UserMessages.Name.Required);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnRequiredFieldForEmail")]
        public void Validate_ShouldReturnRequiredFieldForEmail()
        {
            //Arrange
            var request = new UserCreateRequest
            {
                Name = "User Test",
                Password = "@Password1234",
                RoleId = 1
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.Email).WithErrorMessage(UserMessages.Email.Required);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnInvalidEmailFormat")]
        public void Validate_ShouldReturnInvalidEmailFormat()
        {
            //Arrange
            var request = new UserCreateRequest
            {
                Name = "User Test",
                Email = "invalidEmail",
                Password = "@Password1234",
                RoleId = 1
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.Email).WithErrorMessage(UserMessages.Email.InvalidFormat);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnRequiredFieldForPassword")]
        public void Validate_ShouldReturnRequiredFieldForPassword()
        {
            //Arrange
            var request = new UserCreateRequest
            {
                Name = "User Test",
                Email = "mail@test.com",
                RoleId = 1
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.Password).WithErrorMessage(UserMessages.Password.Required);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnInvalidSizeForPassword")]
        public void Validate_ShouldReturnInvalidSizeForPassword()
        {
            //Arrange
            var request = new UserCreateRequest
            {
                Name = "User Test",
                Email = "mail@test.com",
                Password = "@Pas123",
                RoleId = 1
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.Password).WithErrorMessage(UserMessages.Password.InvalidLength);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnInvalidPasswordFormat")]
        public void Validate_ShouldReturnInvalidPasswordFormat()
        {
            //Arrange
            var request = new UserCreateRequest
            {
                Name = "User Test",
                Email = "mail@test.com",
                Password = "password",
                RoleId = 1
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.Password).WithErrorMessage(UserMessages.Password.InvalidFormat);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnRequiredFieldForRole")]
        public void Validate_ShouldReturnRequiredFieldForRole()
        {
            //Arrange
            var request = new UserCreateRequest
            {
                Name = "User Test",
                Email = "mail@test.com",
                Password = "@Password1234"
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.RoleId).WithErrorMessage(UserMessages.Role.Required);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnMoreThanOneError")]
        public void Validate_ShouldReturnMoreThanOneError()
        {
            //Arrange
            var request = new UserCreateRequest
            {
                Email = "mail@test.com",
                Password = "@Password1234"
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrors();
            result.Errors.Count().Should().BeGreaterThan(1);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldPassTheValidation")]
        public void Validate_ShouldPassTheValidation()
        {
            //Arrange
            var request = new UserCreateRequest
            {
                Name = "User Test",
                Email = "mail@test.com",
                Password = "@Password1234",
                RoleId = 1
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
