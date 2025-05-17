using fiap_fase1_tech_challenge.DTOs.Role;
using fiap_fase1_tech_challenge.Messages;
using fiap_fase1_tech_challenge.Validators.Role;
using FluentValidation.TestHelper;

namespace Tests.UnitTests.Validators.Role
{
    public class RoleUpdateRequestValidatorTests
    {
        private readonly RoleUpdateRequestValidator _validator;

        public RoleUpdateRequestValidatorTests()
        {
            _validator = new RoleUpdateRequestValidator();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "RoleUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnNotEmptyFieldForName")]
        public void Validate_ShouldReturnNotEmptyFieldForName()
        {
            //Arrange
            var request = new RoleUpdateRequest
            {
                Name = "",
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.Name).WithErrorMessage(RoleMessages.Name.CannotBeEmpty);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "RoleUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldPassTheValidation")]
        public void Validate_ShouldPassTheValidation()
        {
            //Arrange
            var request = new RoleUpdateRequest
            {
                Name = "New Name",
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
